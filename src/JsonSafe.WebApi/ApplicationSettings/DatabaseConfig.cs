namespace JsonSafe.WebApi.ApplicationSettings
{
    using Database.Infrastructure;
    using Microsoft.Extensions.Configuration;

    public class DatabaseConfig : IDatabaseConfig
    {
        public DatabaseConfig(IConfiguration configuration)
        {
            Name = configuration.GetValue<string>("name");
            Username = configuration.GetValue<string>("username");
            Password = configuration.GetValue<string>("password");
            Host = configuration.GetValue<string>("host");
            Port = configuration.GetValue<int>("port");
            CollectionName = configuration.GetValue<string>("collectionName");
        }

        public string Name { get; }

        public string Username { get; }

        public string Password { get; }

        public string Host { get; }

        public int Port { get; }

        public string CollectionName { get; }
    }
}
