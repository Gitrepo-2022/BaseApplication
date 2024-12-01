using System.ComponentModel.DataAnnotations;

namespace BaseApplication.Domains
{
    public class ScreenRole
    {
        [Key]
        public int ScreenRoleId { get; set; }
        public int? ScreenId { get; set; }
        public Screen Screen { get; set; }
        public int? RoleId { get; set; }
        public AppIdentityRole Role { get; set; }
    }
}
