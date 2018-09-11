using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReportingTool.Auth
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("reporting_tool_api", "ReportingTool.Api")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "reporting_tool_ui",
                    ClientName = "ReportingTool.UI",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    RedirectUris = { "http://localhost:50112/signin-oidc" },

                    FrontChannelLogoutUri = "http://localhost:50112/signou-oidc" ,
                    PostLogoutRedirectUris = { "http://localhost:50112/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,"reporting_tool_api"
                    },
                }
            };
        }

        //public static List<TestUser> GetUsers()
        //{
        //    return ReportingTool.Auth.Controllers.TestUsers.Users;
        //}
    }
}
