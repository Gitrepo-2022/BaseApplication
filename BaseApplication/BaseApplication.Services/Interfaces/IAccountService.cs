using BaseApplication.Domains;
using BaseApplication.Domains.ViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResultModel> LoginUser(Login model);
        Task<User> GetDataByEmail(String Email);
        Task<ResultModel> SaveUser(UserUIModel model);
        Task<ResultModel> SaveUsertoDb(UserUIModel model);
    }
}


    
