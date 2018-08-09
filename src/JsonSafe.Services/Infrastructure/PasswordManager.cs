namespace JsonSafe.Services.Infrastructure
{
    using System;
    using System.Security.Cryptography;
    using InnerModels;

    public class PasswordManager : IPasswordManager
    {
        private const int IterationsCount = 10000;
        private const int HashBytesCount = 20;

        public PasswordHashSalt GeneratePassword(string password)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[16];
                rngCryptoServiceProvider.GetBytes(saltBytes);
                using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, IterationsCount))
                {
                    var hash = deriveBytes.GetBytes(HashBytesCount);
                    return new PasswordHashSalt(Convert.ToBase64String(hash), Convert.ToBase64String(saltBytes));
                }
            }
        }

        public bool IsPasswordCorrect(string password, string passwordHash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, IterationsCount))
            {
                var hash = deriveBytes.GetBytes(HashBytesCount);
                return string.Equals(passwordHash, Convert.ToBase64String(hash));
            }
        }
    }
}