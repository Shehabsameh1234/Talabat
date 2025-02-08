using Microsoft.AspNetCore.Identity;


namespace Talabat.Core.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayName { get; set; }
        public Address? Address { get; set; }
    }
}
