using System.ComponentModel.DataAnnotations;

namespace BaseApplication.Domains.ViewModels
{
    public class PasswordUIModel
    {
        [Required(ErrorMessage = "New password required", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "The Password must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public int Id { get; set; }

        public bool Access { get; set; }

        public int AspNetUserId { get; set; }
        public string? OldPassword { get; set; }
    }
}
