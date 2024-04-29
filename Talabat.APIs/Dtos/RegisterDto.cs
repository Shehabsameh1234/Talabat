using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[a-zA-Z]).{6,}$",
            ErrorMessage ="password must contain at least one number , one upper case charachter ,one alphanumeric")]
        public string Password { get; set; } = null!;



    }
}
