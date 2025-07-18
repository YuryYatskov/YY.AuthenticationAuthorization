using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer.Data;

public static class DatabaseInitialize
{
    public static void Init(IServiceProvider serviceProvider)
    {
        //var context = serviceProvider.GetService<ApplicationDbContext>();
        var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
        if (userManager == null)
        {
            return;
        }

        var user = new ApplicationUser
        {
            UserName = "Vika",
            //Password = "123qwe",
            FirstName = "Vika",
            LastName = "Doriy",
            Role = "Manager"
        };

        var result = userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();
        if (result.Succeeded)
        {
            userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Manager")).GetAwaiter().GetResult();
        }
        //context.Users.Add(user);
        //context.SaveChanges();

        //serviceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceProvider.GetRequiredService<ConfigurationDbContext>();
        //context.Database.Migrate();
        if (!context.Clients.Any())
        {
            foreach (var client in IdentityServerConfiguration.GetClients())
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiResources.Any())
        {
            foreach (var resource in IdentityServerConfiguration.GetApiResources())
            {
                context.ApiResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in IdentityServerConfiguration.GetApiScopes())
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
    }
}
