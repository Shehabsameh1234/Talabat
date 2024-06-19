﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities;

namespace Talabat.APIs.Extentions
{
    public static class UserManagerExtentions
    {
        public static  async Task<ApplicationUser> FindUserWithAddressAsync(this UserManager<ApplicationUser> userManager,ClaimsPrincipal User )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user =await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.NormalizedEmail == email.ToUpper());
            return user;     
        }
    }
}
