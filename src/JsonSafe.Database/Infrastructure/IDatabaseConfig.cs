namespace JsonSafe.Database.Infrastructure
{
    public interface IDatabaseConfig
    {
        string Name { get; }

        string Username { get; }

        string Password { get; }

        string Host { get; }

        int Port { get; }

        string CollectionName { get; }
    }
}
