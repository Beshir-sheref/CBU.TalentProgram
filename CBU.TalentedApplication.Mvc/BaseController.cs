using CBU.TalentedApplication.Business.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace CBU.TalentedApplication.Mvc
{
    public class BaseController:Controller
    {

        const string sessionKey = "Authenticated_User_Data";
        public string ConvertToMD5(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            byte[] hashData = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(text));

            return Convert.ToBase64String(hashData);
        }

        public User AuthenticatedUser
        {
            get {
                
                if (HttpContext.Session.Keys.Any(x => x == sessionKey))
                {
                    string sessionValue = this.HttpContext.Session.GetString(sessionKey);
                    return JsonConvert.DeserializeObject<User>(sessionValue);
                }

                return null;
            }
            set {
                string serializedString = JsonConvert.SerializeObject(value, Formatting.Indented);
                HttpContext.Session.SetString(sessionKey, serializedString);
            }

        }

        public bool SessionIsValid()
        {
            if (AuthenticatedUser == null)
                return false;
            else
            {
                return true;
            }
        }

        public IActionResult LoadAlerts()
        {
            return PartialView("_AlertMessages");
        }
    }
}
