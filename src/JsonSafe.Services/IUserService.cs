namespace JsonSafe.Services
{
    using System.Threading.Tasks;
    using Models;

    public interface IUserService
    {
        Task<UserModel> CreateAsync(string username, string password, string email);

        Task<UserModel> GetAsync(string username, string password);
    }
}
