using BaseApplication.Domains;
using BaseApplication.Domains.ViewModels;
using BaseApplication.Repository.Interface;
using BaseApplication.Services.Interfaces;

namespace BaseApplication.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
       
        public AccountService(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }
        public async Task<ResultModel> SaveUser(UserUIModel model)
        {
            var res =await _accountRepository.SaveUser(model);
            return res;
        }
        public async Task<ResultModel> LoginUser(Login model)
        {
            var res = await _accountRepository.LoginUser(model);
            return res;
        }
        public async Task<User> GetDataByEmail(String Email)
        {
            var res = await _accountRepository.GetDataByEmail(Email);
            return res;
        }
        public async Task<ResultModel> SaveUsertoDb(UserUIModel model)
        {
            var res = await _accountRepository.SaveUsertoDb(model);
            return res;
        }
    }
}