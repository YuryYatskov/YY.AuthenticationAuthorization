using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace Database
{
    public static class Databaseinitialize
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            //var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            if(userManager == null)
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
        }
    }
}