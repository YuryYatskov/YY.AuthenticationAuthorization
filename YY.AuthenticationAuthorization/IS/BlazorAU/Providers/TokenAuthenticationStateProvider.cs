using BlazorAU.Infrastructure;
using BlazorAU.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorAU.Providers;

public class TokenAuthenticationStateProvider(ILocalStorageService _localStorageService) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        AuthenticationState CreateAnonymous()
        {
            ClaimsIdentity anonymousIdentity = new();
            ClaimsPrincipal anonymousPrincipal = new(anonymousIdentity);
            return new AuthenticationState(anonymousPrincipal);
        }

        var token = await _localStorageService.GetAsync<SecurityToken>(nameof(SecurityToken));

        if (token == null)
        { 
            return CreateAnonymous();
        }
       
        if (string.IsNullOrEmpty(token.AccessToken) || token.ExpiredAt < DateTime.UtcNow)
        {
            return CreateAnonymous();
        }

        // Create real user state.
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Country, "Ukraine"),
            new Claim(ClaimTypes.Name, token.UserName),
            new Claim(ClaimTypes.Expired, token.ExpiredAt.ToLongDateString()),
            new Claim(ClaimTypes.Role, "Administrator"),
            new Claim(ClaimTypes.Role, "Manager"),
            new Claim("Blazor", "Rulez")      
        ];

        ClaimsIdentity identity = new(claims, "Token");
        ClaimsPrincipal principal = new(identity);
        return new AuthenticationState(principal);
    }

    public void MakeUserAnonymous()
    {
        _localStorageService.RemoveAsync(nameof(SecurityToken));

        ClaimsIdentity anonymousIdentity = new();
        ClaimsPrincipal anonymousPrincipal = new(anonymousIdentity);
        var authState =  Task.FromResult(new AuthenticationState(anonymousPrincipal));
        NotifyAuthenticationStateChanged(authState);
    }
}
