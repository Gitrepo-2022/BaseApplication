﻿using BaseApplication.Domains;
using BaseApplication.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Repository.Interface
{
    public interface IAccountRepository
    {
        Task<ResultModel> LoginUser(Login model);
        Task<User> GetDataByEmail(String Email);
        Task<ResultModel> SaveUser(UserUIModel model);
        Task<ResultModel> SaveUsertoDb(UserUIModel model);
    }
}
