using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helper
{
    public class HashManager
    {
        public static string GetHash(string key)
        {
            byte[] salt = new byte[128 / 8] { 122, 148, 145, 158, 187, 1, 255, 198, 22, 48, 182, 214, 98, 99, 45, 201, };

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: key,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
