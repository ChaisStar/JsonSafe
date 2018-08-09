namespace JsonSafe.WebApi.ApplicationSettings
{
    using Microsoft.Extensions.Configuration;
    using Services.Infrastructure;

    public class JwtTokenConfig: IJwtTokenConfig
    {
        public JwtTokenConfig(IConfiguration configuration)
        {
            Key = configuration.GetValue<string>("key");
            Issuer = configuration.GetValue<string>("issuer");
            Lifetime = configuration.GetValue<int>("lifetime");
        }

        public string Key { get; }

        public string Issuer { get; }

        public int Lifetime { get; }
    }
}
