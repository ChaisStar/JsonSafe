namespace JsonSafe.Database
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IJsonRepository
    {
        Task AddJsonAsync(string username, JsonModel jsonModel);

        Task<IEnumerable<JsonModel>> GetUserJsonsAsync(string username);

        Task<JsonModel> GetJsonAsync(string username, Guid jsonId);

        Task DeleteJsonAsync(string username, Guid jsonId);
    }
}