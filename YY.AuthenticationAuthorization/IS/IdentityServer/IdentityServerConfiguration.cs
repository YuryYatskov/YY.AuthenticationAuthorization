using System.Collections.Generic;
using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {                              
            new Client
            {
                ClientId = "client_id",
                ClientSecrets = { new Secret("client_secret".ToSha256()) },
                
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    "OrdersApi",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };

        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource("OrdersApi");
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            yield return new ApiScope("OrdersApi", "Orders API");
        }
    }
}
