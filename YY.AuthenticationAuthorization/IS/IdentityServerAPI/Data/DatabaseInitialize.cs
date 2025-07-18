using Duende.IdentityModel;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityServerAPI.Entities;
using IdentityServerAPI.IdentityServerConfiguration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServerAPI.Data;

public class DatabaseInitialize(
    UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IServiceProvider serviceProvider) : IDatabaseInitialize
{

    public void Initialize()
    {
        if (roleManager.FindByNameAsync(Config.Admin).Result == null)
        {
            roleManager.CreateAsync(new ApplicationRole(Config.Admin)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new ApplicationRole(Config.Customer)).GetAwaiter().GetResult();
        }
        else
            return;

        ApplicationUser adminUser = new()
        {
            UserName = "Vika",
            //FirstName = "Vika",
            //LastName = "Doriy",
            //Role = "Manager"
        };

        userManager.CreateAsync(adminUser, "123qwe").GetAwaiter().GetResult();
        userManager.AddToRoleAsync(adminUser, Config.Admin).GetAwaiter().GetResult();

        var claims1 = userManager.AddClaimsAsync(adminUser,
            [
                new Claim(JwtClaimTypes.Name, adminUser.UserName),
                new Claim(JwtClaimTypes.Role, Config.Admin)
            ]).Result;

        ApplicationUser customerUser = new()
        {
            UserName = "Cus",
            //Email = "customer10@gmail.com",
            //EmailConfirmed = true,
            //PhoneNumber = "380677654321",
            //Name = "Ben Customer",
        };

        userManager.CreateAsync(customerUser, "123qwe").GetAwaiter().GetResult();
        userManager.AddToRoleAsync(customerUser, Config.Customer).GetAwaiter().GetResult();

        var claims2 = userManager.AddClaimsAsync(customerUser,
            [
                new Claim(JwtClaimTypes.Name, customerUser.UserName),
                new Claim(JwtClaimTypes.Role, Config.Customer)
            ]).Result;

        //serviceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceProvider.GetRequiredService<ConfigurationDbContext>();
        //context.Database.Migrate();

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Config.IdentityResources)
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiResources.Any())
        {
            foreach (var resource in Config.ApiResources)
            {
                context.ApiResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in Config.ApiScopes)
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }
    }
}
