using System;

namespace NekoUchi.Model
{
    public class User
    {
        #region Properties
        public string Password { get; set; }
        public string Basil { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime RegistrationDate { get; set; }
        #endregion
    }
}
