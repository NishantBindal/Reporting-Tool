using IdentityModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReportingTool.Auth.Presistance
{
    public class UserStore : IUserStore
{
        private string connectionString;
        public UserStore(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ReportingToolAuth");
        }

        public async Task<bool> ValidateCredentials(string username, string password)
        {
            string hash = null;
            string salt = null;
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                DataTable table = new DataTable();
                using (var cmd = new SqlCommand("SELECT [PasswordSalt], [PasswordHash] FROM [User] WHERE [Username] = @username;", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@username", username));
                    var reader = await cmd.ExecuteReaderAsync();
                    table.Load(reader);
                    reader.Close();
                }
                if (table.Rows.Count > 0)
                {
                    salt = (string)(table.Rows[0]["PasswordSalt"]);
                    hash = (string)(table.Rows[0]["PasswordHash"]);
                }
            }
            return (String.IsNullOrEmpty(salt) || String.IsNullOrEmpty(hash)) ? false : User.PasswordValidation(hash, salt, password);
        }

        public async Task<User> FindBySubjectId(string subjectId)
        {
            User user = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE [SubjectId] = @subjectid;"))
            {
                cmd.Parameters.Add(new SqlParameter("@subjectid", subjectId));
                user = await ExecuteFindCommand(cmd);
            }
            return user;
        }

        public async Task<User> FindByUsername(string username)
        {
            User user = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE [Username] = @username;"))
            {
                cmd.Parameters.Add(new SqlParameter("@username", username));
                user = await ExecuteFindCommand(cmd);
            }
            return user;
        }

        public async Task<User> FindByExternalProvider(string provider, string subjectId)
        {
            User user = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE [ProviderName] = @pname AND [ProviderSubjectId] = @psub;"))
            {
                cmd.Parameters.Add(new SqlParameter("@pname", provider));
                cmd.Parameters.Add(new SqlParameter("@psub", subjectId));
                user = await ExecuteFindCommand(cmd);
            }
            return user;
        }

        private async Task<User> ExecuteFindCommand(SqlCommand cmd)
        {
            User user = null;
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                cmd.Connection = conn;
                var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    reader.Close();
                    var userRow = table.Rows[0];
                    user = new User()
                    {
                        id = (int)userRow["UserId"],
                        SubjectId = (string)userRow["SubjectId"],
                        Username = (string)userRow["Username"],
                        PasswordSalt = (string)userRow["PasswordSalt"],
                        PasswordHash = (string)userRow["PasswordHash"],
                        ProviderName = (string)userRow["ProviderName"],
                        ProviderSubjectId = (string)userRow["ProviderSubjectId"],
                    };
                    using (var claimcmd = new SqlCommand("SELECT * FROM [Claim] WHERE [UserId] = @uid;", conn))
                    {
                        claimcmd.Parameters.Add(new SqlParameter("@uid", user.id));
                        reader = await claimcmd.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            table = new DataTable();
                            table.Load(reader);
                            user.Claims = new List<Claim>(table.Rows.Count);
                            foreach (DataRow row in table.Rows)
                            {
                                user.Claims.Add(new Claim(
                                    type: (string)row["Type"],
                                    value: (string)row["Value"],
                                    valueType: (string)row["ValueType"],
                                    issuer: (string)row["Issuer"],
                                    originalIssuer: (string)row["OriginalIssuer"]));
                            }
                        }
                        reader.Close();
                    }
                }
                cmd.Connection = null;
            }
            return user;
        }

        public async Task<User> AutoProvisionUser(string provider, string subjectId, List<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>();

            foreach (var claim in claims)
            {
                // if the external system sends a display name - translate that to the standard OIDC name claim
                if (claim.Type == ClaimTypes.Name)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
                }
                // if the JWT handler has an outbound mapping to an OIDC claim use that
                else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
                {
                    filtered.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
                }
                // copy the claim as-is
                else
                {
                    filtered.Add(claim);
                }
            }

            // if no display name was provided, try to construct by first and/or last name
            if (!filtered.Any(x => x.Type == JwtClaimTypes.Name))
            {
                var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value;
                var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }

            // create a new unique subject id
            var sub = CryptoRandom.CreateUniqueId();

            // check if a display name is available, otherwise fallback to subject id
            var name = filtered.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value ?? sub;

            // create new user
            var user = new User
            {
                SubjectId = sub,
                Username = name,
                ProviderName = provider,
                ProviderSubjectId = subjectId,
                Claims = filtered
            };

            // store it and give it back
            await SaveUser(user);
            return user;
        }

        public async Task<bool> SaveUser(User user, string newPasswordToHash = null)
        {
            bool success = true;
            if (!String.IsNullOrEmpty(newPasswordToHash))
            {
                user.PasswordSalt = User.PasswordSaltInBase64();
                user.PasswordHash = User.PasswordToHashBase64(newPasswordToHash, user.PasswordSalt);
            }
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string upsert =
                        $"MERGE [User] WITH (ROWLOCK) AS [T] " +
                        $"USING (SELECT {user.id} AS [UserId]) AS [S] " +
                        $"ON [T].[UserId] = [S].[UserId] " +
                        $"WHEN MATCHED THEN UPDATE SET [SubjectId]='{user.SubjectId}', [Username]='{user.Username}', [PasswordHash]='{user.PasswordHash}', [PasswordSalt]='{user.PasswordSalt}', [ProviderName]='{user.ProviderName}', [ProviderSubjectId]='{user.ProviderSubjectId}' " +
                        $"WHEN NOT MATCHED THEN INSERT ([SubjectId],[Username],[PasswordHash],[PasswordSalt],[ProviderName],[ProviderSubjectId]) " +
                        $"VALUES ('{user.SubjectId}','{user.Username}','{user.PasswordHash}','{user.PasswordSalt}','{user.ProviderName}','{user.ProviderSubjectId}'); " +
                        $"SELECT SCOPE_IDENTITY();";
                    object result = null;
                    using (var cmd = new SqlCommand(upsert, conn))
                    {
                        result = await cmd.ExecuteScalarAsync();
                    }
                    int newId = (result is null || result is DBNull) ? 0 : Convert.ToInt32(result); // SCOPE_IDENTITY returns a SQL numeric(38,0) type
                    if (newId > 0) user.id = newId;
                    if (user.id > 0 && user.Claims.Count > 0)
                    {
                        foreach (Claim c in user.Claims)
                        {
                            string insertIfNew =
                                $"MERGE [Claim] AS [T] " +
                                $"USING (SELECT {user.id} AS [UserId], '{c.Subject}' AS [sub], '{c.Type}' AS [type], '{c.Value}' as [val]) AS [S] " +
                                $"ON [T].[UserId]=[S].[uid] AND [T].[Subject]=[S].[sub] AND [T].[Type]=[S].[type] AND [T].[Value]=[S].[val] " +
                                $"WHEN NOT MATCHED THEN INSERT ([UserId],[Issuer],[OriginalIssuer],[Subject],[Type],[Value],[ValueType]) " +
                                $"VALUES ('{user.id}','{c.Issuer ?? string.Empty}','{c.OriginalIssuer ?? string.Empty}','{user.SubjectId}','{c.Type}','{c.Value}','{c.ValueType ?? string.Empty}');";
                            using (var cmd = new SqlCommand(insertIfNew, conn))
                            {
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                    }
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }

        public Task<bool> SaveAppUser(User user, string newPasswordToHash = null)
        {
            throw new NotImplementedException();
        }
    }
}
