namespace JsonSafe.Services.Infrastructure
{
    public interface IJwtTokenConfig
    {
        string Key { get; }

        string Issuer { get; }

        int Lifetime { get; }
    }
}
