using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityServer.Entities;

public class ApplicationUser : IdentityUser<Guid>  
{
    //public Guid Id { get; set; }

    //public string UserName { get; set; } = string.Empty;
   
    //public string Password { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
   
    public string LastName { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    //public List<string> Roles { get; set; } = new List<string>();
}
