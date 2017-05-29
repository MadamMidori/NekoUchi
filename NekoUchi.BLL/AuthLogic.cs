using NekoUchi.BLL.Helpers;
using NekoUchi.DAL;
using NekoUchi.DAL.Helpers;
using System;
using System.Text;

namespace NekoUchi.BLL
{
    public class AuthLogic
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Register()
        {
            // Setup user
            var user = new User();
            try
            {
                // Generate password hash first
                string salt = Randoms.GenerateSalt(8);
                string hash = Hasher.GetHash(Password, salt);
                user.ModelUser = new Model.User();
                user.ModelUser.Email = Email;
                user.ModelUser.Password = hash;
                user.ModelUser.Basil = salt;
                user.ModelUser.RegistrationDate = DateTime.Now;
                user.ModelUser.Role = Constants.Roles.User;
                IDataProvider data = new MongoDataProvider();
                if (!data.Create(user.ModelUser))
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            // Get a session token
            string sessionToken = GetToken(user.ModelUser.Email);

            //// if everything passed well
            return sessionToken;
        }

        public string Login()
        {
            IDataProvider data = new MongoDataProvider();
            // Get the user info
            var user = data.Get<DAL.User>("Email", Email);

            // Now hashing the given password and compare hashes
            string hash = Hasher.GetHash(Password, user.Basil);

            // Correct - setup token
            if (user.Password == hash)
            {
                string sessionToken = GetToken(user.Email);
                return sessionToken;
            }
            // False - return error
            else
            {
                return "";
            }
        }

        public static bool Logout()
        {
            // Get the user info
            // Clean session
            throw new NotImplementedException();
        }

        public static string GetToken(string email)
        {
            // Save to session collection
            var session = new Session()
            {
                Email = email,
                Role = Constants.Roles.User,
                TTE = DateTime.Now + TimeSpan.FromDays(1)
            };
            session.Create();
            return session.Id;
        }

        public static string CheckToken(string token)
        {
            IDataProvider data = new MongoDataProvider();
            // Fetch session from DB
            var session = data.Get<Session>("Id", token);

            // if there is any, check TTE            
            if (session != null && session.Id != "")
            {
                // if TTE is alright, return email of the current user
                if (session.TTE > DateTime.Now)
                {
                    return session.Email;
                }
                else
                {
                    DeleteToken(token);
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public static bool DeleteToken(string token)
        {
            IDataProvider data = new MongoDataProvider();
            try
            {
                return data.Delete<Session>("Id", token);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
