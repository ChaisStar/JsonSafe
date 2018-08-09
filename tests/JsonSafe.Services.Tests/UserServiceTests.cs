namespace JsonSafe.Services.Tests
{
    using System;
    using System.Threading.Tasks;
    using AutoFixture.NUnit3;
    using Database;
    using InnerModels;
    using JsonSafe.Infrastructure.Exceptions.BusinessLogicExceptions;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Services.Infrastructure;

    [TestFixture]
    public class UserServiceTests
    {
        private IPasswordManager _passwordManager;
        private IUserRepository _userRepository;
        private UserService _userServiceInstance;
        private IApiKeyGenerator _apiKeyGenerator;

        [SetUp]
        public void Setup()
        {
            _passwordManager = Substitute.For<IPasswordManager>();
            _userRepository = Substitute.For<IUserRepository>();
            _apiKeyGenerator = Substitute.For<IApiKeyGenerator>();
            _userServiceInstance = new UserService(_passwordManager, _userRepository, _apiKeyGenerator);
        }

        [Test, AutoData]
        public void CreateAsync_Should_Throw_UsernameExistException_If_Username_Exist(string username)
        {
            _userRepository.IsUsernameExistAsync(username).Returns(true);
            Assert.ThrowsAsync<UsernameExistException>(async () => await _userServiceInstance.CreateAsync(username, null, null));
        }

        [Test, AutoData]
        public void CreateAsync_Should_Throw_EmailExistException_If_Email_Exist(string username, string email)
        {
            _userRepository.IsUsernameExistAsync(username).Returns(false);
            _userRepository.IsEmailExistAsync(email).Returns(true);
            Assert.ThrowsAsync<EmailExistException>(async () => await _userServiceInstance.CreateAsync(username, null, email));
        }

        [Test, AutoData]
        public async Task CreateAsync_Should_Add_User_CorrectlyAsync(string username, string email, string password,
            PasswordHashSalt passwordHashSalt, string apiKey)
        {
            var user = default(UserModel);
            _apiKeyGenerator.GenerateNewApiKey().Returns(apiKey);
            _userRepository.IsUsernameExistAsync(username).Returns(false);
            _userRepository.IsEmailExistAsync(email).Returns(false);
            _passwordManager.GeneratePassword(password).Returns(passwordHashSalt);
            _userRepository.When(x => x.AddUserAsync(Arg.Any<UserModel>())).Do(x => user = (UserModel)x[0]);
            await _userServiceInstance.CreateAsync(username, password, email);
            Assert.IsNotNull(user);
            Assert.AreEqual(username, user.Username);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(passwordHashSalt.PasswordHash, user.PasswordHash);
            Assert.AreEqual(passwordHashSalt.Salt, user.Salt);
            Assert.That(DateTime.UtcNow, Is.EqualTo(user.Created).Within(1).Seconds);
            Assert.That(DateTime.UtcNow, Is.EqualTo(user.Updated).Within(1).Seconds);
            Assert.AreEqual(apiKey, user.ApiKey);
        }

        [Test, AutoData]
        public void GetAsync_Should_Throw_InvalidCredentialsException_If_User_Not_Found(string username)
        {
            _userRepository.GetByUsernameAsync(username).Returns(null as UserModel);
            Assert.ThrowsAsync<InvalidCredentialsException>(async () => await _userServiceInstance.GetAsync(username, null));
        }

        [Test, AutoData]
        public void GetAsync_Should_Throw_InvalidCredentialsException_If_Password_Incorrect(string username, string password, UserModel userModel)
        {
            _userRepository.GetByUsernameAsync(username).Returns(userModel);
            _passwordManager.IsPasswordCorrect(password, userModel.PasswordHash, userModel.Salt).Returns(false);
            Assert.ThrowsAsync<InvalidCredentialsException>(async () => await _userServiceInstance.GetAsync(username, password));
        }

        [Test, AutoData]
        public async Task GetAsync_Should_Return_User_If_Password_Correct(string username, string password, UserModel userModel)
        {
            _userRepository.GetByUsernameAsync(username).Returns(userModel);
            _passwordManager.IsPasswordCorrect(password, userModel.PasswordHash, userModel.Salt).Returns(true);
            var user = await _userServiceInstance.GetAsync(username, password);
            Assert.AreEqual(userModel, user);
        }
    }
}
