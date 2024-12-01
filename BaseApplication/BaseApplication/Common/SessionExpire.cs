using BaseApplication.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static BaseApplication.Web.Common.GetLoggedUserData;

namespace BaseApplication.Utility
{
    public class SessionExpire:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var data = filterContext.HttpContext.Session.GetData<LoggedUserData>("UserData");
            if (data == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
            return;
        }
    }
}
