using Azure;
using BaseApplication.Domains;
using BaseApplication.Domains.ViewModels;
using BaseApplication.Repository.Data;
using BaseApplication.Repository.Interface;
using BaseApplication.Utility;
using Microsoft.AspNetCore.Identity;
using Snickler.EFCore;
using System;
using System.Collections.Generic;
using static Azure.Core.HttpHeader;

namespace BaseApplication.Repository.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BaseApplicationDbContext _entity;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountRepository(BaseApplicationDbContext entity, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _entity = entity;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<ResultModel> LoginUser(Login model)
        {
            var response = new ResultModel();
            List<User> users = new List<User>();
            try
            {

                await _entity.LoadStoredProc(spConstant.spGetUserDetail)
                .WithSqlParam("@Email", model.Email)
                .ExecuteStoredProcAsync((handler) =>
                {
                    users = (List<User>)handler.ReadToList<User>();
                });
                var user = users != null && users.Count > 0 ? users.FirstOrDefault() : null;
                // var user = _entity.Users.Where(x => x.Email == model.Email && !x.IsDeleted && x.IsActive).FirstOrDefault();  //using Linq
                if (user == null)
                {
                    response.response = "User Not Found";
                    response.success = false;
                    return response;
                }

                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password,
                    isPersistent: false, lockoutOnFailure: false);


                if (result.Succeeded)
                {
                    response.success = true;
                    response.response = "success";
                    return response;


                }
                else
                {
                    response.response = "Email Already Exist";
                    response.success = false;
                    return response;
                }
            }
            catch (Exception e)
            {
                response.response = e.Message;
                response.success = false;
            }

            return response;
        }
        public async Task<ResultModel> SaveUser(UserUIModel model)
        {
            ResultModel resultModel = new ResultModel();
            try
            {
                ApplicationUser applicationuser = new ApplicationUser();
                applicationuser.Email = model.Email;
                applicationuser.UserName = model.Email;
                applicationuser.NormalizedEmail = model.Email.ToUpper();
                applicationuser.NormalizedUserName = model.Email.ToUpper();
                applicationuser.PhoneNumber = model.ContactNumber;
                applicationuser.TenantId = 2;
                var result = await _userManager.CreateAsync(applicationuser);
                if (result.Succeeded == true)
                {
                    var getId = _userManager.FindByNameAsync(applicationuser.UserName);
                    resultModel.id = getId.Result.Id;
                    await _signInManager.SignInAsync(applicationuser, isPersistent: false);
                    resultModel.success = true;
                    return resultModel;
                }
                else
                {
                    resultModel.response = "EmailExist";
                    resultModel.success = false;
                    return resultModel;
                }

                return resultModel;
            }
            catch (Exception e)
            {
                resultModel.success = false;
                resultModel.response = e.Message;
                return resultModel;
            }
        }

        public async Task<User> GetDataByEmail(String Email)
        {
            User user = new User();
            await _entity.LoadStoredProc(spConstant.spGetUserDetail)
               .WithSqlParam("@Email", Email)
               .ExecuteStoredProcAsync((handler) =>
               {
                   var users = (List<User>)handler.ReadToList<User>();
                   user=users.FirstOrDefault(); 
               });
            return user;
        }

        public async Task<ResultModel> SaveUsertoDb(UserUIModel model)
        {
            var resultmodel = new ResultModel();
            List<ResultModel> result = new List<ResultModel>();
            try
            {

                await _entity.LoadStoredProc(spConstant.spSaveUser)
                    .WithSqlParam("@Id", model.Id)
                    .WithSqlParam("@FirstName", model.FirstName)
                    .WithSqlParam("@LastName", model.LastName)
                    .WithSqlParam("@Email", model.Email)
                    .WithSqlParam("@IsActive", model.IsActive)
                    .WithSqlParam("@AspNetUserId", model.AspNetUserId)
                    .WithSqlParam("@City", model.City)
                    .WithSqlParam("@State", model.State)
                    .WithSqlParam("@Address", model.Address)
                    .WithSqlParam("@Zipcode", model.Zipcode)
                    .WithSqlParam("@ContactNumber", model.ContactNumber)
                    .WithSqlParam("@CreatedById", model.CreatedById)
                    .WithSqlParam("@ModifiedById", model.ModifiedById)
                    .ExecuteStoredProcAsync((handler) =>
                    {
                        result = (List<ResultModel>)handler.ReadToList<ResultModel>();
                        resultmodel.response = result[0].response;
                    });
                resultmodel.success = true;
                return resultmodel;
            }
            catch (Exception e)
            {
                resultmodel.success = false;
                resultmodel.response = e.Message;
                return resultmodel;
            }


        }
    }
}
