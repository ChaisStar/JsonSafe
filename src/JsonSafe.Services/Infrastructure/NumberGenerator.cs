namespace JsonSafe.Services.Infrastructure
{
    using System.Security.Cryptography;

    public class NumberGenerator : INumberGenerator
    {
        public byte[] GetBytes()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            return key;
        }
    }
}