using IdentityServerAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{
    //public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    //public DbSet<ApplicationRole> ApplicationRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
}
