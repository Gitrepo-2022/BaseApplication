using BaseApplication.Repository.Data;
using BaseApplication.Services.Interfaces;

namespace BaseApplication.Web.Common
{
    public static class CommonServices
    {
        public static BaseApplicationDbContext _entity;
        public static IAccountService _accountService;
        public static IEmailService _emailService;
        public static IUserService _userService;
    }
}
