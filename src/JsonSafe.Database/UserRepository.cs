namespace JsonSafe.Database
{
    using DbModels;

    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Models;

    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollectionClient<DbUser> _mongoCollectionClient;
        private readonly IMapper _mapper;
        private const string CollectionName = nameof(DbUser);

        public UserRepository(IMongoCollectionClient<DbUser> mongoCollectionClient, IMapper mapper)
        {
            _mongoCollectionClient = mongoCollectionClient;
            _mapper = mapper;
        }

        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            await Task.WhenAll(
                _mongoCollectionClient.InsertOneAsync(CollectionName, _mapper.Map<DbUser>(user)),
                _mongoCollectionClient.CreateCollectionAsync(user.Username));
            return await GetByUsernameAsync(user.Username);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            await Task.WhenAll(
                _mongoCollectionClient.DeleteOneAsync(CollectionName, x => x.Id == userId),
                _mongoCollectionClient.DropCollectionAsync(user.Username));
            return true;
        }

        public async Task<UserModel> GetByIdAsync(Guid userId) =>
            _mapper.Map<UserModel>(await _mongoCollectionClient.FindOneAsync(CollectionName, x => x.Id == userId));

        public async Task<UserModel> GetByUsernameAsync(string username) =>
            _mapper.Map<UserModel>(await _mongoCollectionClient.FindOneAsync(CollectionName, x => x.Username == username));

        public async Task<UserModel> GetByEmailAsync(string email) =>
            _mapper.Map<UserModel>(await _mongoCollectionClient.FindOneAsync(CollectionName, x => x.Email == email));

        public Task<bool> IsEmailExistAsync(string email) =>
            _mongoCollectionClient.ExistAsync(CollectionName, x => x.Email == email);

        public Task<bool> IsUsernameExistAsync(string username) =>
            _mongoCollectionClient.ExistAsync(CollectionName, x => x.Username == username);
    }
}