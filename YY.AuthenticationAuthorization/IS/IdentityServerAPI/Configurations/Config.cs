using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServerAPI.IdentityServerConfiguration;

public static class Config
{
    public const string Admin = "admin";
    public const string Customer = "customer";

    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
    ];
    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("OrdersApi", "Orders API"),
        new ApiResource("ClientMvc", "Client MVC")
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("OrdersApi", "Orders API"),
        new ApiScope("ClientMvc", "Client MVC")
    ];

    public static IEnumerable<Client> Clients =>
    [
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
        },
        new Client
        {
            ClientId = "client_id_mvc",
            ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            AllowedScopes =
            {
                "OrdersApi",
                //"UserApi"
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            },

            RedirectUris = { "https://localhost:7025/signin-oidc" },
            PostLogoutRedirectUris = { "https://localhost:7025/signout-callback-oidc" },
            RequireConsent = false,

            //AlwaysIncludeUserClaimsInIdToken = true,

            AccessTokenLifetime = 5,

            AllowOfflineAccess = true,
        }
    ];
}
