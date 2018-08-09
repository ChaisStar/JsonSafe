namespace JsonSafe.Database.Tests.Infrastructure
{
    using System.Threading.Tasks;
    using AutoFixture.NUnit3;
    using Database.Infrastructure;
    using MongoDB.Driver;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class MongoCollectionClientTests
    {
        private IMongoDatabase _mongoDatabase;
        private IDatabaseMongoClient _databaseMongoClient;
        private IDatabaseConfig _databaseConfig;
        private IMongoClient _mongoClient;
        private MongoCollectionClient<object> _mongoCollectionClientInstance;
        private IMongoCollection<object> _mongoCollection;

        [SetUp]
        public void Setup()
        {
            _mongoDatabase = Substitute.For<IMongoDatabase>();
            _databaseMongoClient = Substitute.For<IDatabaseMongoClient>();
            _databaseConfig = Substitute.For<IDatabaseConfig>();
            _mongoClient = Substitute.For<IMongoClient>();
            _mongoCollection = Substitute.For<IMongoCollection<object>>();
            _databaseMongoClient.MongoClient.Returns(_mongoClient);
            _mongoClient.GetDatabase(_databaseConfig.Name).Returns(_mongoDatabase);
            _mongoCollectionClientInstance = new MongoCollectionClient<object>(_databaseMongoClient, _databaseConfig);
        }

        private void SetupCollection(string collectionName) =>
            _mongoDatabase.GetCollection<object>(collectionName).Returns(_mongoCollection);

        [Test, AutoData]
        public async Task CreateCollectionAsync_Should_Call_Database_To_Create_CollectionAsync(string collectionName)
        {
            await _mongoCollectionClientInstance.CreateCollectionAsync(collectionName);
            await _mongoDatabase.Received().CreateCollectionAsync(collectionName);
        }

        [Test, AutoData]
        public async Task DropCollectionAsync_Should_Call_Database_To_Drop_CollectionAsync(string collectionName)
        {
            await _mongoCollectionClientInstance.DropCollectionAsync(collectionName);
            await _mongoDatabase.Received().DropCollectionAsync(collectionName);
        }

        [Test, AutoData]
        public async Task InsertOneAsync_Should_Call_Collection_To_Insert_ElementAsync(string collectionName, object entity)
        {
            SetupCollection(collectionName);
            await _mongoCollectionClientInstance.InsertOneAsync(collectionName, entity);
            await _mongoCollection.Received().InsertOneAsync(entity);
        }
    }
}
