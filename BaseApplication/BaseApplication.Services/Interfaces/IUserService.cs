using BaseApplication.Domains;
using BaseApplication.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserPaginationUIModel> GetUserList(UserPaginationUIModel model);

        public Task<UserUIModel>GetUserById(int id);
    }
}
