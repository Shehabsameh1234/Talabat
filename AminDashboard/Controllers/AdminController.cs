using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace AminDashboard.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto logIn)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(logIn.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email");
                    return RedirectToAction(nameof(LogIn));
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, logIn.Password, false);
                if (!result.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    ModelState.AddModelError(string.Empty, "you are not authorized");
                    return RedirectToAction(nameof(LogIn));

                }
                else
                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(LogIn));
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
    }
}
