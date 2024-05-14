namespace AminDashboard.Models
{
    public class UserRoleViewModel
    {
        public string UseriD { get; set; } =null!;
        public string UsernAME { get; set; } = null!;
        public List<RoleViewModel> Roles { get; set; } = null!;


    }
}
