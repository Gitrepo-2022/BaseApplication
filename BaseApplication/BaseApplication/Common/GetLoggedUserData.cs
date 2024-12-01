namespace BaseApplication.Web.Common
{
    public class GetLoggedUserData
    {
        public IHttpContextAccessor httpContextAccessor;

        public GetLoggedUserData(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public LoggedUserData UserDetail
        {
            get
            {
                if (this.httpContextAccessor.HttpContext.Session.GetData<LoggedUserData>("UserData") != null)
                {
                    return this.httpContextAccessor.HttpContext.Session.GetData<LoggedUserData>("UserData");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                var d = this.httpContextAccessor.HttpContext.Session.GetData<LoggedUserData>("UserData");
                d = value;
            }
        }

        public class LoggedUserData
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }
    }
}
