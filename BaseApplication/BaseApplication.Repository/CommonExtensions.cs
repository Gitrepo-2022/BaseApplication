using BaseApplication.Domains;
using BaseApplication.Repository.Data;
using Microsoft.AspNetCore.Identity;

namespace BaseApplication.Repository
{
    public static class CommonExtensions
    {
        public static  BaseApplicationDbContext _entity;
        public static SignInManager<ApplicationUser> _signInManager;
        public static UserManager<ApplicationUser> _userManager;
       
    }
}
