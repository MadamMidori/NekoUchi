using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NekoUchi.DAL.Helpers
{
    public class Randoms
    {
        public static string GenerateSalt(int saltLength)
        {
            var rndNumGen = RandomNumberGenerator.Create();
            byte[] salt = new byte[saltLength];
            rndNumGen.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}
