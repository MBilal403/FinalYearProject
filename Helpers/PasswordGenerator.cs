using System;
using System.Linq;
using System.Security.Cryptography;

namespace FYP.Helpers
{
   

    public static class PasswordGenerator
    {
        private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=";

        public static string GeneratePassword(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[length];
                rng.GetBytes(bytes);

                var chars = bytes.Select(b => AllowedChars[b % AllowedChars.Length]);
                return new string(chars.ToArray());
            }
        }
    }

}