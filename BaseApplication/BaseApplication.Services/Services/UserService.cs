using BaseApplication.Domains;
using BaseApplication.Domains.ViewModels;
using BaseApplication.Repository.Interface;
using BaseApplication.Services.Interfaces;
using System.Collections.Generic;

namespace BaseApplication.Services.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userrepository;
        public UserService(IUserRepository userrepository)
        {
            _userrepository = userrepository;   
        }
        public async Task<UserPaginationUIModel> GetUserList(UserPaginationUIModel model)
        {
            var res = await _userrepository.GetUserList(model);
            return res;
        }
        public async Task<UserUIModel> GetUserById(int id)
        {
            var res = await _userrepository.GetUserById(id);
            return res;
        }
    }
}
