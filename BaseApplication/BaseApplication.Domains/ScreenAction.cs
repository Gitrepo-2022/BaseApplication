using System.ComponentModel.DataAnnotations;

namespace BaseApplication.Domains
{
    public class ScreenAction
    {
        [Key]
        public int ScreenActionId { get; set; }
        public string ScreenActionName { get; set; }
        public int ScreenId { get; set; }
        public Screen Screen { get; set; }
    }
}
