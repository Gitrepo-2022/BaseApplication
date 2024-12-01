using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Domains
{
    public class User:DomainBase
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
        public int AspNetUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zipcode { get; set; }
    }
}
