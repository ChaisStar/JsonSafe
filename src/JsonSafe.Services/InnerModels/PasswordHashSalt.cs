namespace JsonSafe.Services.InnerModels
{
    public class PasswordHashSalt
    {
        public PasswordHashSalt(string passwordHash, string salt)
        {
            PasswordHash = passwordHash;
            Salt = salt;
        }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }
    }
}
