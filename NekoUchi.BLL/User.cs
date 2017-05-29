using NekoUchi.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NekoUchi.BLL
{
    public class User
    {
        #region Properties        
        public Model.User ModelUser { get; set; }
        public List<Model.User> ModelUsers { get; set; }
        #endregion

        #region Static methods

        public static User Get(string email)
        {
            try
            {
                IDataProvider data = new MongoDataProvider();
                var user = new User();              
                user.ModelUser =  data.Get<DAL.User>("Email", email);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
