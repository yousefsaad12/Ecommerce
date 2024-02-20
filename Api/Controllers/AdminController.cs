using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("seeding")]
        public async Task<IActionResult> RoleSeeding()
        {   
            bool userRole = await _roleManager.RoleExistsAsync(StaticAppUserRoles.USER);
            bool adminRole = await _roleManager.RoleExistsAsync(StaticAppUserRoles.ADMIN);

            if(userRole && adminRole)
                return BadRequest("Roles are already Seeded");

            await _roleManager.CreateAsync(new IdentityRole(StaticAppUserRoles.USER));
            await _roleManager.CreateAsync(new IdentityRole(StaticAppUserRoles.ADMIN));

            return Ok("Roles has been Seeded");
        }
    }
}