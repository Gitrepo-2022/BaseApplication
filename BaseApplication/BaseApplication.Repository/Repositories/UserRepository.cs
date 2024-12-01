using BaseApplication.Domains;
using BaseApplication.Domains.ViewModels;
using BaseApplication.Repository.Data;
using BaseApplication.Repository.Interface;
using BaseApplication.Utility;
using Microsoft.AspNetCore.Identity;
using Snickler.EFCore;

namespace BaseApplication.Repository.Repositories
{
    
    public class UserRepository:IUserRepository
    {
        //private readonly BaseApplicationDbContext _entity;
        public UserRepository(BaseApplicationDbContext entity, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            CommonExtensions._entity = entity;
            CommonExtensions._signInManager = signInManager;
            CommonExtensions._userManager = userManager;
        }
        public async Task<UserPaginationUIModel> GetUserList(UserPaginationUIModel model)
        {
            try
            {
                var data = new UserPaginationUIModel();
                await CommonExtensions._entity.LoadStoredProc(spConstant.spGetUserList)
                .WithSqlParam("@PageIndex", model.PageIndex)
                .WithSqlParam("@PageSize", model.PageSize)
                .WithSqlParam("@SortColm", model.SortColm)
               .WithSqlParam("@SortOrder", model.SortOrder)
                .WithSqlParam("@FirstName", model.FirstName)
                 .WithSqlParam("@LastName", model.LastName)
                  .WithSqlParam("@Email", model.Email)
                .ExecuteStoredProcAsync((handler) =>
                {
                    data.Users = (List<UserUIModel>)handler.ReadToList<UserUIModel>();
                });
                data.PageSize = model.PageSize;
                data.PageIndex=model.PageIndex;
                data.TotalRecordCount= (data.Users != null && data.Users.Count> 0)?data.Users[0].TotalRecordCount:0;
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<UserUIModel> GetUserById(int id)
        {
            try
            {
                var model = new UserUIModel();
                var user = CommonExtensions._entity.Users.Where(x => x.Id == id && !x.IsDeleted && x.IsActive).FirstOrDefault();
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.ContactNumber = user.ContactNumber;
                model.Id = user.Id;
                model.Address = user.Address;
                model.City = user.City ?? "";
                model.State = user.State ?? "";
                model.Zipcode = user.Zipcode ?? "";
                model.IsActive = user.IsActive;
                model.AspNetUserId = user.AspNetUserId;
                return model;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
