namespace JsonSafe.Services.Infrastructure
{
    using Models;

    public interface IJwtTokenService
    {
        string CreateToken(UserModel user);
    }
}
