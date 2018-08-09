namespace JsonSafe.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DbModels;
    using Models;

    public class JsonRepository : IJsonRepository
    {
        private readonly IMongoCollectionClient<DbJson> _mongoCollectionClient;
        private readonly IMapper _mapper;

        public JsonRepository(IMongoCollectionClient<DbJson> mongoCollectionClient, IMapper mapper)
        {
            _mongoCollectionClient = mongoCollectionClient;
            _mapper = mapper;
        }

        public Task AddJsonAsync(string username, JsonModel jsonModel) =>
            _mongoCollectionClient.InsertOneAsync(username, _mapper.Map<DbJson>(jsonModel));

        public async Task<IEnumerable<JsonModel>> GetUserJsonsAsync(string username) =>
            (await _mongoCollectionClient.FindAsync(username, x => true)).Select(x => _mapper.Map<JsonModel>(x));

        public async Task<JsonModel> GetJsonAsync(string username, Guid jsonId) =>
            _mapper.Map<JsonModel>(await _mongoCollectionClient.FindOneAsync(username, x => x.Id == jsonId));

        public Task DeleteJsonAsync(string username, Guid jsonId) =>
            _mongoCollectionClient.DeleteOneAsync(username, x => x.Id == jsonId);
    }
}
