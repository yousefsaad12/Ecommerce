using Api.Core.Domain;
using Api.Roles;
using Microsoft.AspNetCore.Identity;

namespace api.Repository
{
    public class RolesRepo : IRolesInterface
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesRepo(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> SeedingRoles()
        {   
            
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticAppUserRoles.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticAppUserRoles.USER);

            if (isAdminRoleExists && isUserRoleExists)
                return false;

            await _roleManager.CreateAsync(new IdentityRole(StaticAppUserRoles.USER));
            await _roleManager.CreateAsync(new IdentityRole(StaticAppUserRoles.ADMIN));

            return true;
        }
    }
}