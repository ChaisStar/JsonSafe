namespace JsonSafe.Database.Infrastructure
{
    using MongoDB.Driver;

    public interface IDatabaseMongoClient
    {
        IMongoClient MongoClient { get; }
    }
}