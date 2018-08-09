namespace JsonSafe.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IJsonService
    {
        Task<bool> CreateAsync(string userName, JsonModel jsonModel);

        Task<JsonModel> GetDataAsync(string userName, Guid id);

        Task DeleteDataAsync(string userName, Guid id);

        Task<IEnumerable<JsonModel>> GetAllDataAsync(string userName);
    }
}
