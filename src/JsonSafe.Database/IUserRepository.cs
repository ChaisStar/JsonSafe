namespace JsonSafe.Database
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IUserRepository
    {
        Task<UserModel> AddUserAsync(UserModel user);

        Task<bool> DeleteUserAsync(Guid userId);

        Task<UserModel> GetByIdAsync(Guid userId);

        Task<UserModel> GetByUsernameAsync(string username);

        Task<UserModel> GetByEmailAsync(string email);

        Task<bool> IsEmailExistAsync(string email);

        Task<bool> IsUsernameExistAsync(string username);
    }
}