using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.UserDTOS;
using Api.Interfaces;
using Api.Mappers;
using Api.Models;
using Api.Roles;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserInterface _userInterface;
        private readonly ITokenInterface _tokenInterface;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenInterface tokenInterface, IUserInterface userInterface)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenInterface = tokenInterface;
            _userInterface = userInterface;
        }
        
        [AllowAnonymous]
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsers()
        {   
            List<User> users = await _userInterface.GetUsers();
            var usersResponse = users.Select(u => u.ToUserResponse());
            
            return Ok(usersResponse);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel userRegister)
        {   
           try
           { 
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var exist = await _userManager.FindByEmailAsync(userRegister.Email);

                if(exist != null)
                    return BadRequest("this email is alraedy has been used");

                User user = new User()
                {
                    Email = userRegister.Email,
                    UserName = userRegister.UserName,
                    Address = userRegister.Address,
                    PhoneNumber = userRegister.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, userRegister.Password);

                if(result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user,StaticAppUserRoles.USER);

                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new UserResponse
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                Address = user.Address,
                                Token = _tokenInterface.GenerateJwtToke(user, StaticAppUserRoles.USER),
                            }
                        );
                    }

                    else return StatusCode(500,roleResult.Errors);
                }

                else return StatusCode(500, result.Errors);
           }

           catch(Exception e)
           {
             return StatusCode(500, e);
           }
           
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult>Login([FromBody] UserLoginDTO userLoginDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == userLoginDTO.Email.ToLower());

            if(appUser == null)
                return Unauthorized("Invalid Username or Password");

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, userLoginDTO.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Invalid Username or Password");

            var user =  await _userInterface.GetUser(appUser.Id);

            return Ok(
                new UserResponse 
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Token = _tokenInterface.GenerateJwtToke(user, StaticAppUserRoles.USER)
                    
                }
            );
            
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult>LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok("User logged out.");
        }

        



    }
}