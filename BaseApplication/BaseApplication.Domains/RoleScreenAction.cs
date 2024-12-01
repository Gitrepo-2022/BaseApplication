using System.ComponentModel.DataAnnotations;

namespace BaseApplication.Domains
{
    public class RoleScreenAction
    {
        [Key]
        public int RoleScreenActionId { get; set; }
        public int ScreenRoleId { get; set; }
        public ScreenRole ScreenRole { get; set; }
        public int? ScreenActionId { get; set; }
        public ScreenAction ScreenAction { get; set; }
    }
}
