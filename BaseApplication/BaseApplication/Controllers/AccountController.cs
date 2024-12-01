using BaseApplication.Domains;
using BaseApplication.Domains.ViewModels;
using BaseApplication.Services.Interfaces;
using BaseApplication.Utility;
using BaseApplication.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using static BaseApplication.Web.Common.LoggedUserDetail;

namespace BaseApplication.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        public AccountController(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IEmailService emailService, IUserService userService)
        {
             _httpContextAccessor=httpContextAccessor;
            _userManager = userManager;
            //_accountService = accountService;
            _emailService = emailService;
            CommonServices._userService=userService;
        }

        [SessionExpire]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            Login model = new Login();
            if (Request.Cookies.ContainsKey("Email") == true)
            {
                model.Email = Request.Cookies["Email"];
                model.Password =
                    Request.Cookies["Password"];
                model.RememberMe = true;
            }
            else
            {
                model.RememberMe = false;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                //var res = _accountService.LoginUser(model);
                //if (res.Result.success)
                //{
                //    ApplicationUser applicationUser = await _userManager.FindByEmailAsync(model.Email);
                //    SetLoggedInUserAsync(applicationUser.Email);
                //    if (model.RememberMe)
                //    {
                //        CookieOptions option = new CookieOptions();
                //        option.Expires = DateTime.Now.AddDays(15);
                //        Response.Cookies.Append("Email", model.Email, option);
                //        Response.Cookies.Append("Password", model.Password, option);

                //    }
                //    return RedirectToAction("index", "User");
                //}
                return View(model);

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        private void SetLoggedInUserAsync(string Email)
        {
            //var result = _accountService.GetDataByEmail(Email);
            //HttpContext.Session.SetData("UserData", result.Result);
            //CookieOptions option = new CookieOptions();
            //option.Expires = DateTime.Now.AddDays(15);
            //Response.Cookies.Append("FirstName", result.Result.FirstName, option);
            //Response.Cookies.Append("LastName", result.Result.LastName, option);
            //Response.Cookies.Append("Id", result.Result.Id.ToString(), option);
        }

        [SessionExpire]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [SessionExpire]
        [HttpPost]
        //public async Task<IActionResult> ForgotPassword(ForgetPassword model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _accountService.GetDataByEmail(model.Email);
        //        if (user != null)
        //        {
        //            var appuser = await _userManager.FindByIdAsync(user.AspNetUserId.ToString());
        //            var code = await _userManager.GeneratePasswordResetTokenAsync(appuser);
        //            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //            //var request = _httpContextAccessor.HttpContext.Request;
        //            var _baseURL = CommonHelpers.BaseUrl;
        //            MailTemplate mail = new MailTemplate();
        //            mail.ToEmail = model.Email;
        //            mail.Subject = "Reset Password";
        //            mail.Body = $"<div style ='margin: 0;auto;max-width:700px;'><table class='' style='width:70%;margin:40px auto;font-size:0px;background:#ececec;'><tbody><tr>" +
        //                 "<td style = 'text-align:center;vertical-align:top;font-size:0;padding:1px;'><div style='vertical-align:top;display:inline-block;font-size:13px;text-align:left;width:100%;'>" +
        //                 "<table style = 'background:white;width:100%;padding-bottom:20px;' ><tbody><tr><td style='font-size:0;padding:20px 30px 20px;border-bottom: 1px solid #ece6e6;font-weight: 600;'>" +
        //                 "<div style = 'cursor:auto;color:#776acf;font-family:Proxima Nova, Arial, Arial, Helvetica, sans-serif;font-size:22px;line-height:22px;text-align:center;'>" +
        //                 mail.Subject + "</div></td> </tr>" + "<tr> <td style = 'font-size:0;padding:15px 30px 16px;' ><div style='cursor:auto;color:#000000;font-family:Proxima Nova, Arial, Arial, Helvetica, sans-serif;font-size:14px;line-height:22px;'>" +
        //                  "Hey" + "<b> " + user.FirstName + " " + user.LastName + " </b></div></td></tr>" +
        //                 "<tr><td style = 'font-size:0;padding:0 30px 6px;' ><div style= 'cursor:auto;color:#000000;font-family:Proxima Nova, Arial, Arial, Helvetica, sans-serif;font-size:14px;line-height:22px;'>" +
        //                 "ResetPassBody" + "</div></td></tr><tr>" + "<td><a href=\'" + _baseURL + "/Account/ResetPassword/?UserId=" + code +
        //                 "&Email=" + user.Email + "\' style= 'display:inline-block;text-decoration:none;background:#776acf !important;border:1px solid #776acf;border-radius:3px;color:white;font-family:Proxima Nova, Arial, Arial, Helvetica, sans-serif;font-size:14px;font-weight:400;text-align: center;width:100%;padding: 11px 0px 11px 0px;' target= '_blank'>" + "Reset Password" + "</a>" +
        //                 "</td></tr>" + "</tbody></table><div>";
        //            var res = await _emailService.SendEmailAsync(mail);
        //            return RedirectToAction("SentLink", "Account");
        //        }
        //        ViewBag.Message = "Please enter valid Email";
        //        return View(model);
        //    }

        //    return View();
        //}
        [SessionExpire]
        public IActionResult SentLink()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult ResetPassword(string userid, string Email)
        {
            try
            {
                PasswordUIModel model = new PasswordUIModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(userid)),
                    Email = Email
                };
                return View(model);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpire]
        public async Task<ActionResult> ResetPassword(PasswordUIModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);
                    if (result.Succeeded)
                    {

                        user = await _userManager.FindByEmailAsync(model.Email);
                        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        if (model.Access)
                        {
                            return Json(code);
                        }
                        else
                        {
                            return RedirectToAction("Login", "Account");
                        }
                    }
                }
            }
            return View(model);
        }

        [SessionExpire]
        [HttpGet]
        public async Task<IActionResult> CreateUser(int id=0)
        {
            var model = new UserUIModel();
            if(id> 0)
            {
                model = await CommonServices._userService.GetUserById(id);
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser()
        {
            UserUIModel model = new UserUIModel();
            var result = new ResultModel();
            var data = HttpContext.Session.GetData<LoggedUser>("UserData");
            if (ModelState.IsValid)
            {
                if (model != null && model.Id == 0)
                {
                    //var res = await _accountService.SaveUser(model);
                    //if (res.success)
                    //{
                    //    //model.CreatedById = res.id;
                    //    model.AspNetUserId = res.id.Value;
                    //    result = await _accountService.SaveUsertoDb(model);
                    //}
                }
                else
                {
                    model.ModifiedById = data.Id;
                   // result = await _accountService.SaveUsertoDb(model);
                }
                return RedirectToAction("Index", "User");
            }
            return View(model);
        }
    }
}
