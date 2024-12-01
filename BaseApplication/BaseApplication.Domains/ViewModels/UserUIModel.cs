using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Domains.ViewModels
{
    public class UserUIModel
    {
        public int Id { get; set; } = 0;
        [Required]
        public string FirstName { get; set; } = "Test";
        [Required]
        public string LastName { get; set; } = "User";
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = "Test@yopmail.com";
        [Required]
        public bool IsActive { get; set; }=true;
        public int AspNetUserId { get; set; }=0;
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? ContactNumber { get; set; } = "7863645354";
        [Required]
        public string? City { get; set; } = "Ambala";
        [Required]
        public string? State { get; set; } = "Hrayana";
        [Required]
        public string? Zipcode { get; set; } = "133203";
        public int TotalRecordCount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int? CreatedById { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public int? DeletedBy { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }

    }

    public class UserPaginationUIModel
    {
        public List<UserUIModel> Users { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecordCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SortColm { get; set; }
        public string SortOrder { get; set; }
    }
}
