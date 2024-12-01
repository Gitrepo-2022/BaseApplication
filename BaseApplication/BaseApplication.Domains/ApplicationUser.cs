using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApplication.Domains
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int TenantId { get; set; }
        public Tenant Tenants { get; set; }

    }
    public class ForgetPassword
    {
        [NotMapped]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}