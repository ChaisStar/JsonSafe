namespace JsonSafe.Database.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Driver;

    public class MongoCollectionClient<T> : IMongoCollectionClient<T>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoCollectionClient(IDatabaseMongoClient databaseMongoClient, IDatabaseConfig databaseConfig)
        {
            _mongoDatabase = databaseMongoClient.MongoClient.GetDatabase(databaseConfig.Name);
        }

        public Task CreateCollectionAsync(string collectionName) =>
            _mongoDatabase.CreateCollectionAsync(collectionName);

        public Task DropCollectionAsync(string collectionName) =>
            _mongoDatabase.DropCollectionAsync(collectionName);

        public Task InsertOneAsync(string collectionName, T entity) =>
            GetCollection(collectionName).InsertOneAsync(entity);

        [ExcludeFromCodeCoverage]
        public async Task<IEnumerable<T>> FindAsync(string collectionName, Expression<Func<T, bool>> expression) =>
            (await GetCollection(collectionName).FindAsync(expression)).ToEnumerable();

        [ExcludeFromCodeCoverage]
        public async Task<T> FindOneAsync(string collectionName, Expression<Func<T, bool>> expression) =>
            (await GetCollection(collectionName).FindAsync(expression)).FirstOrDefault();

        [ExcludeFromCodeCoverage]
        public async Task<bool> ExistAsync(string collectionName, Expression<Func<T, bool>> expression) =>
            await GetCollection(collectionName).CountDocumentsAsync(expression) > 0;

        [ExcludeFromCodeCoverage]
        public Task DeleteOneAsync(string collectionName, Expression<Func<T, bool>> expression) =>
            GetCollection(collectionName).DeleteOneAsync(expression);

        private IMongoCollection<T> GetCollection(string collectionName) =>
            _mongoDatabase.GetCollection<T>(collectionName);
    }
}
