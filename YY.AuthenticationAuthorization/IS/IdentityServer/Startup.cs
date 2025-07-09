using IdentityServer.Data;
using IdentityServer.Entities;
using IdentityServer.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseInMemoryDatabase("MEMORY");
        })
            .AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer(options =>
        {
            options.UserInteraction.LoginUrl = "/Auth/Login";
        })
            .AddAspNetIdentity<ApplicationUser>()
            .AddInMemoryClients(IdentityServerConfiguration.GetClients())
            .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
            .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
            .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
            .AddProfileService<ProfileService>()
            .AddDeveloperSigningCredential();

        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseIdentityServer();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}
