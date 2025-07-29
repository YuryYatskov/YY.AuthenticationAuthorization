using BlazorAU;
using BlazorAU.Infrastructure;
using BlazorAU.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
