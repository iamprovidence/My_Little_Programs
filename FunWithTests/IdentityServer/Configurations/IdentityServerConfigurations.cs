using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer.Configurations
{
    public class IdentityServerConfigurations
    {
        private const string ServerScope = "server";

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource(ServerScope, "Web API Server, v1"),
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope(ServerScope),
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "swagger",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        ServerScope,
                    },
                    RedirectUris = { string.Empty },
                    PostLogoutRedirectUris = { string.Empty },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                },
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "admin",
                    Password = "admin",
                    IsActive = true,
                },
            };
        }
    }
}
