using System.ComponentModel.DataAnnotations;

namespace BaseApplication.Domains
{
    public class Screen
    {
        [Key]
        public int ScreenId { get; set; }
        public string ScreenName { get; set; }
    }
}
