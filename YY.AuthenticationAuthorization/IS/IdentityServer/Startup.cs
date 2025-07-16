using IdentityServer.Data;
using IdentityServer.Entities;
using IdentityServer.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace IdentityServer;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            //config.UseInMemoryDatabase("MEMORY"); <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.18"
            config.UseSqlServer(configuration.GetConnectionString("Database"));
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

        services.ConfigureApplicationCookie(config =>
        {
            config.LoginPath = "/Auth/Login";
            config.LogoutPath = "/Auth/Logout";
            config.Cookie.Name = "IdentityServer.Cookies";
        });

        var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        services.AddIdentityServer()
        //    options =>
        //{
        //    options.UserInteraction.LoginUrl = "/Auth/Login";
        //})
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(configuration.GetConnectionString("Database"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(configuration.GetConnectionString("Database"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            //.AddInMemoryClients(IdentityServerConfiguration.GetClients())
            //.AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
            //.AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
            //.AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
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
