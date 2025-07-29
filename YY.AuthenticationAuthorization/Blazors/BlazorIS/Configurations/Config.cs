using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace BlazorIS.Configurations;

public static class Config
{
    public const string Admin = "admin";
    public const string Customer = "customer";

    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Address(),
        new IdentityResources.Email(),
    ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("API", "Server API"),
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("API", "Server API"),
    ];

    public static IEnumerable<Client> Clients =>
    [
        new Client
        {
            ClientId = "client_blazor",
            RequireClientSecret = false,
            RequireConsent = false,
            RequirePkce = true,
            AllowedGrantTypes = GrantTypes.Code,
            AllowedScopes =
            {
                "API",
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Address,
                IdentityServerConstants.StandardScopes.Email,
            },
            RedirectUris = { "https://localhost:7001/authentication/login-callback" },
            PostLogoutRedirectUris = { "https://localhost:7001/authentication/logout-callback" },
            AllowedCorsOrigins = { "https://localhost:7001" }
        },
    ];
}
