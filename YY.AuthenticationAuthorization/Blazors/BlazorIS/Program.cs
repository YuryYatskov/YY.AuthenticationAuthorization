using BlazorIS.Data;
using BlazorIS.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

SeedData.EnsureSeedData(app);

app.Run();

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddRazorPages();

//builder.Services.AddAuthentication();

//builder.Services.AddAuthorization();

//builder.Services.AddIdentityServer()
//    .AddInMemoryApiResources(Config.ApiResources)
//    .AddInMemoryClients(Config.Clients)
//    .AddInMemoryIdentityResources(Config.IdentityResources)
//    .AddInMemoryApiScopes(Config.ApiScopes)
//    .AddDeveloperSigningCredential();

//builder.Services.AddCors();

//var app = builder.Build();

//app.UseCors(builder =>
//{
//    builder.AllowAnyHeader()
//    .AllowAnyMethod()
//    .AllowAnyOrigin();
//});

//app.UseAuthentication();
//app.UseAuthorization();
//app.UseIdentityServer();

//app.MapRazorPages()
//    .RequireAuthorization();

//app.Run();
