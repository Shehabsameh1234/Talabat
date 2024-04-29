using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.Core.Entities;
using Talabat.Core.Service.Contract;
using Talabat.Srevice.AuthService;

namespace Talabat.APIs.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAuthService authService,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
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
                Token =await _authService.CreateTokenAsync(user,_userManager),
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
                Token = await _authService.CreateTokenAsync(user, _userManager),
            });
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpGet("address")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            if (user.Address == null) return Ok(new {
                result="there is no address for this user"
            });
            return Ok(_mapper.Map<AddressDto>(user.Address));
        }
        [HttpPut("updateaddress")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Address>> UpdateUserAddress(AddressDto addressDto)
        {
            var updatedAddress = _mapper.Map<Address>(addressDto);
            var user = await _userManager.FindUserWithAddressAsync(User);
            if(user.Address is not null) updatedAddress.id = user.Address.id;
            user.Address = updatedAddress;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApisValidationErrors() { Errors = result.Errors.Select(e => e.Description) });
            return Ok(updatedAddress);
        }

    }
}
