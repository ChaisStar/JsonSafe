namespace JsonSafe.WebApi.ApplicationSettings
{
    using Microsoft.Extensions.Configuration;

    public class AppSettings : IAppSettings
    {
        public AppSettings(IConfiguration configuration)
        {
            Database = new DatabaseConfig(configuration.GetSection("database"));
            JwtToken = new JwtTokenConfig(configuration.GetSection("jwtToken"));
        }

        /// <inheritdoc />
        public DatabaseConfig Database { get; }

        /// <inheritdoc />
        public JwtTokenConfig JwtToken { get; }
    }
}