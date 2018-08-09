namespace JsonSafe.Services
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Infrastructure;
    using JsonSafe.Infrastructure.Exceptions.BusinessLogicExceptions;
    using Models;

    public class UserService : IUserService
    {
        private readonly IPasswordManager _passwordManager;
        private readonly IUserRepository _userRepository;
        private readonly IApiKeyGenerator _apiKeyGenerator;

        public UserService(IPasswordManager passwordManager, IUserRepository userRepository, IApiKeyGenerator apiKeyGenerator)
        {
            _passwordManager = passwordManager;
            _userRepository = userRepository;
            _apiKeyGenerator = apiKeyGenerator;
        }

        public async Task<UserModel> CreateAsync(string username, string password, string email)
        {
            if (await _userRepository.IsUsernameExistAsync(username))
            {
                throw new UsernameExistException();
            }

            if (await _userRepository.IsEmailExistAsync(email))
            {
                throw new EmailExistException();
            }

            var passwordHashSalt = _passwordManager.GeneratePassword(password);
            var user = new UserModel
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHashSalt.PasswordHash,
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Salt = passwordHashSalt.Salt,
                ApiKey = _apiKeyGenerator.GenerateNewApiKey()
            };
            return await _userRepository.AddUserAsync(user);
        }

        /// <inheritdoc />
        public async Task<UserModel> GetAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new InvalidCredentialsException();
            }

            var isPasswordCorrect = _passwordManager.IsPasswordCorrect(password, user.PasswordHash, user.Salt);
            return isPasswordCorrect ? user : throw new InvalidCredentialsException();
        }
    }
}