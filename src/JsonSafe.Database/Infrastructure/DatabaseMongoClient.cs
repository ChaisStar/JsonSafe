namespace JsonSafe.Database.Infrastructure
{
    using MongoDB.Driver;

    public class DatabaseMongoClient : IDatabaseMongoClient
    {
        public DatabaseMongoClient(IDatabaseConfig databaseConfig)
        {
            MongoClient = new MongoClient(new MongoClientSettings
            {
                Credential = MongoCredential.CreateCredential(databaseConfig.Name, databaseConfig.Username, databaseConfig.Password),
                Server = new MongoServerAddress(databaseConfig.Host, databaseConfig.Port)
            });
        }

        public IMongoClient MongoClient { get; }
    }
}
