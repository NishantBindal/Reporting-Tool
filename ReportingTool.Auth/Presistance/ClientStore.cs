using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingTool.Auth.Presistance
{
    public class ClientStore : IClientStore
    {
        private string connectionString;
        public ClientStore(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ReportingToolAuth");
        }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            Client client = null;
            using (SqlCommand cmd = new SqlCommand("SELECT C.ClientName AS CLIENTNAME,C.ClientSecrets AS CLIENTSECRETS,RedirectUri,FrontChannelLogoutUri,PostLogoutRedirectUri,C.CLIENTID AS CLIENTID,G.GrantType AS GrantType,S.ScopeType AS ScopeType FROM [Client] AS C INNER JOIN[CLIENTGRANT] AS CG ON C.ID = CG.ClientId INNER JOIN[ClientScope] AS CS ON C.ID = CS.ClientId INNER JOIN[GRANT] G ON G.GrantId = CG.AllowedGrantId INNER JOIN Scope S ON S.ScopeId = CS.AllowedScopeId WHERE C.[ClientId] = @clientid;"))
            {
                cmd.Parameters.Add(new SqlParameter("@clientid", clientId));
                client = await ExecuteFindCommand(cmd);
            }
            return client;
        }
        private async Task<Client> ExecuteFindCommand(SqlCommand cmd)
        {
            Client client = null;
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
                    var clientRow = table.Rows[0];
                    client = new Client()
                    {
                        ClientId = (string)clientRow["ClientId"],
                        ClientName = (string)clientRow["ClientName"],
                        ClientSecrets =new List<Secret>() { new Secret(((string)clientRow["ClientSecrets"]).Sha256())},
                        RedirectUris = new List<string>() { (string)clientRow["RedirectUri"] },
                        PostLogoutRedirectUris = new List<string>() { (string)clientRow["PostLogoutRedirectUri"] },
                        FrontChannelLogoutUri = (string)clientRow["FrontChannelLogoutUri"],
                        AllowedScopes = table.Select().Select(dr => (string)dr["ScopeType"]).ToList(),
                        AllowedGrantTypes = table.Select().Select(dr => (string)dr["GrantType"]).Distinct().ToList()
                    };
                }
                cmd.Connection = null;
            }
            return client;
        }
    }
}
