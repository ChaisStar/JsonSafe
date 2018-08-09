namespace JsonSafe.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IMongoCollectionClient<T>
    {
        Task CreateCollectionAsync(string collectionName);
        Task DropCollectionAsync(string collectionName);
        Task InsertOneAsync(string collectionName, T entity);
        Task<IEnumerable<T>> FindAsync(string collectionName, Expression<Func<T, bool>> expression);
        Task<T> FindOneAsync(string collectionName, Expression<Func<T, bool>> expression);
        Task<bool> ExistAsync(string collectionName, Expression<Func<T, bool>> expression);
        Task DeleteOneAsync(string collectionName, Expression<Func<T, bool>> expression);
    }
}
