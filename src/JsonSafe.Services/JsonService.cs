namespace JsonSafe.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Models;

    public class JsonService : IJsonService
    {
        private readonly IJsonRepository _jsonRepository;

        public JsonService(IJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }

        public async Task<bool> CreateAsync(string userName, JsonModel jsonModel)
        {
            await _jsonRepository.AddJsonAsync(userName, jsonModel);
            return true;
        }

        public Task<JsonModel> GetDataAsync(string userName, Guid id) => _jsonRepository.GetJsonAsync(userName, id);

        public Task DeleteDataAsync(string userName, Guid id) => _jsonRepository.DeleteJsonAsync(userName, id);

        public Task<IEnumerable<JsonModel>> GetAllDataAsync(string userName) => _jsonRepository.GetUserJsonsAsync(userName);
    }
}