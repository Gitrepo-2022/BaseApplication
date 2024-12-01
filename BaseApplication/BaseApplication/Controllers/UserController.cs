using BaseApplication.Domains.ViewModels;
using BaseApplication.Repository.Data;
using BaseApplication.Services.Interfaces;
using BaseApplication.Web.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using static BaseApplication.Web.Common.LoggedUserDetail;

namespace BaseApplication.Web.Controllers
{
    public class UserController : Controller
    {
        //private readonly IUserService _userService;
        //private readonly IAccountService _accountService;
        public UserController( BaseApplicationDbContext entity)
        {
            CommonServices._entity = entity;
            //CommonServices._userService = userService;
            //_accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        //public async Task<IActionResult> GetUserList(UserPaginationUIModel Pagination)
        //{
        //    var result = await CommonServices._userService.GetUserList(Pagination);
        //    return Json(result);
        //}

        //public void DeleteUserById(int id)
        //{
        //    var deleteduser = CommonServices._entity.Users.Where(x => x.Id == id).FirstOrDefault();
        //    var data = HttpContext.Session.GetData<LoggedUser>("UserData");
        //    if (deleteduser != null && deleteduser.Id > 0)
        //    {
        //        deleteduser.DeletedBy = data.Id;
        //        deleteduser.DeletedDate=DateTimeOffset.Now;
        //        deleteduser.IsDeleted = true;
        //    }
        //    CommonServices._entity.SaveChanges();
        //}
        public async Task<JsonResult>GetData(string id)
        {
            try
            {
                var plantext = DecryptStringAES(id);
                return Json(plantext);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        public static string DecryptStringAES(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using(var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
    }
}
