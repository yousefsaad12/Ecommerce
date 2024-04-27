using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.AdminDTOS;
using Api.Interfaces;
using Api.Models;
using Api.Roles;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenInterface _tokenInterface;

        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, ITokenInterface tokenInterface, SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _tokenInterface = tokenInterface;
            _signInManager = signInManager;
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

        [HttpPost("Regsiter")]
        public async Task<IActionResult> Register([FromBody]AdminRegsiterModel adminRegsiter)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var exist = await _userManager.FindByEmailAsync(adminRegsiter.Email);

                if(exist != null)
                    return BadRequest("this email is alraedy has been used");

                var admin = new Admin ()
                {
                    UserName = adminRegsiter.UserName,
                    Email = adminRegsiter.Email,
                };

                var result = await _userManager.CreateAsync(admin, adminRegsiter.Password);

                if(result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(admin, StaticAppUserRoles.ADMIN);

                    if(roleResult.Succeeded)
                    {
                        return Ok
                        (
                            new AdminResponseDTO 
                            {
                                UserName = admin.UserName,
                                Email = admin.Email,
                                Token = _tokenInterface.GenerateJwtToke(admin, StaticAppUserRoles.ADMIN)
                            }
                        );
                    }

                    else return StatusCode(500, roleResult.Errors);
                }

                else return StatusCode(500, result.Errors);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

         [HttpPost("Login")]
         public async Task<IActionResult> Login([FromBody]AdminLoginDTO adminLogin)
         {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var admin = await _userManager.FindByEmailAsync(adminLogin.Email);

            if(admin == null)
                return Unauthorized("Email or password is invalid");

            var result = await _signInManager.CheckPasswordSignInAsync(admin, adminLogin.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Email or password is invalid");

            return Ok(
                new AdminResponseDTO
                {
                    UserName = admin.UserName,
                    Email = admin.Email,
                    Token = _tokenInterface.GenerateJwtToke(admin, StaticAppUserRoles.ADMIN)

                }
            );
         }
    }
}