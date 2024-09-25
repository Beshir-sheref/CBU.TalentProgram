using CBU.TalentedApplication.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Repo
{
    public class UserRepo : Repo<User>
    {
        public User GetUserdata(string email, string password)
        {
            if (email == null || password == null) { return null; }

           return Search(x => x.Email == email).Single();
        }

        public bool ValidateUser(string email, string password)
        {
            if (email == null || password == null) { return false; }
            string hashedPassword = ConvertToMD5(password);

            if (Search(x => x.Email == email && x.Password == hashedPassword).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //email validation for LostPassword and SignUp 
        public bool ValidateEmail(string email)
        {

            if (email == null) { return false; }

            if (Search(x => x.Email == email).Any())
            {
                return true ;
            }
            else
            {
                return false;
            }
                
        }

    }
}
