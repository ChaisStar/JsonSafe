namespace JsonSafe.Services.Infrastructure
{
    using InnerModels;

    public interface IPasswordManager
    {
        PasswordHashSalt GeneratePassword(string password);

        bool IsPasswordCorrect(string password, string passwordHash, string salt);
    }
}
