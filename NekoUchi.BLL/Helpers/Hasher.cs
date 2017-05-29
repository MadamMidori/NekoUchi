using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace NekoUchi.BLL.Helpers
{
    public class Hasher
    {       
        public static string GetHash(string text, string salt)
        {            
            byte[] saltb = Encoding.UTF8.GetBytes(salt);
            byte[] rawHash = KeyDerivation.Pbkdf2(text, saltb, KeyDerivationPrf.HMACSHA256, 10000, 256 / 8);
            string hash = Encoding.UTF8.GetString(rawHash);
            return hash;
        }
    }
}
