using BlazorAU.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace BlazorAU.ViewModels;

public class LoginModel : ComponentBase 
{
    [Inject]
    public ILocalStorageService LocalStorageService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public LoginViewModel LoginData { get; set; }

    public LoginModel()
    {
        LoginData = new LoginViewModel();
    }

    public async Task LoginAsync()
    {
        var token = new SecurityToken
        { 
            AccessToken = LoginData.Password,
            UserName = LoginData.UserName,
            ExpiredAt = DateTime.UtcNow.AddDays(1)
        };

        await LocalStorageService.SetAsync(nameof(SecurityToken), token);

        NavigationManager.NavigateTo("/", true);
    }
}

public class SecurityToken
{
    public string UserName { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public DateTime ExpiredAt { get; set; }
}
