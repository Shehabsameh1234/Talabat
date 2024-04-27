using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Identity
{
    public static class IdentityContextSeedingData
    {
        public static async Task IdentitySeedAsync(UserManager<ApplicationUser> _userManager)
        {
          
            if(!_userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "shehab",
                    Email = "shehabsameh987123@gmail.com",
                    PhoneNumber = "01147218434",
                    UserName = "shehab.sameh",
                };
                await _userManager.CreateAsync(user,"Shehab123@");
            }
        }
    }
}
