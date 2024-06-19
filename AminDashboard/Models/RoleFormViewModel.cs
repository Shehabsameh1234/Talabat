using System.ComponentModel.DataAnnotations;

namespace AminDashboard.Models
{
    public class RoleFormViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}
