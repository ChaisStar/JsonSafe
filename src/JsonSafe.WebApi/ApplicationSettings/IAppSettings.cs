namespace JsonSafe.WebApi.ApplicationSettings
{
    public interface IAppSettings
    {
        DatabaseConfig Database { get; }

        JwtTokenConfig JwtToken { get; }
    }
}
