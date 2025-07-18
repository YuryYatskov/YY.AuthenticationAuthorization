using Microsoft.AspNetCore.Identity;

namespace IdentityServerAPI.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }

        public ApplicationRole(string roleName) => base.Name = roleName;
    }
}
