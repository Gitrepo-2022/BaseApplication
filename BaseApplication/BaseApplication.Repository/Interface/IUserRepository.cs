using BaseApplication.Domains;
using BaseApplication.Domains.ViewModels;

namespace BaseApplication.Repository.Interface
{
    public interface IUserRepository
    {
        public  Task<UserPaginationUIModel> GetUserList(UserPaginationUIModel model);
        public Task<UserUIModel> GetUserById(int id);
    }
}
