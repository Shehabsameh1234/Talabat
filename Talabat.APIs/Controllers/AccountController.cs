using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;

namespace Talabat.APIs.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn(LogInDto model)
        {
            var user =await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApisResponse(401, "Invalid LogIn"));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if(!result.Succeeded) return Unauthorized(new ApisResponse(401, "Invalid LogIn"));

            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = "This Will Be Our Token",
            });
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split("@")[0],
            };
            var result=await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new ApisValidationErrors() {Errors=result.Errors.Select(e=>e.Description)});
            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = "This Will Be Our Token",
            });
        }
    }
}
