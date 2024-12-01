using BaseApplication.Domains;
using Newtonsoft.Json;
using System.Text;

namespace BaseApplication.Web.Common
{
    public static class LoggedUserDetail
    {
        public static T GetData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public class LoggedUser
        {
            public ApplicationUser application;

            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }
            public int? Roleid { get; set; }
            public string? RoleName { get; set; }

        }
    }
}
