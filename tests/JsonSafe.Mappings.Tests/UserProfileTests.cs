namespace JsonSafe.Mappings.Tests
{
    using System;
    using AutoFixture.NUnit3;
    using AutoMapper;
    using DbModels;
    using Dtos.UserModels;
    using Models;
    using NUnit.Framework;

    [TestFixture]
    public class UserProfileTests
    {
        private readonly IMapper _mapper;

        public UserProfileTests()
        {
            var mapperConfiguration = new MapperConfiguration(x => x.AddProfile<UserProfile>());
            mapperConfiguration.AssertConfigurationIsValid();
            _mapper = new Mapper(mapperConfiguration);
        }

        [Test, AutoData]
        public void DbUser_To_UserModel_Mapping_Should_Work_Correctly(DbUser dbUser)
        {
            var userModel = _mapper.Map<UserModel>(dbUser);
            Assert.AreEqual(dbUser.Id, userModel.Id);
            Assert.AreEqual(dbUser.Created, userModel.Created);
            Assert.AreEqual(dbUser.Updated, userModel.Updated);
            Assert.AreEqual(dbUser.ApiKey, userModel.ApiKey);
            Assert.AreEqual(dbUser.Email, userModel.Email);
            Assert.AreEqual(dbUser.PasswordHash, userModel.PasswordHash);
            Assert.AreEqual(dbUser.Salt, userModel.Salt);
            Assert.AreEqual(dbUser.Username, userModel.Username);
        }

        [Test, AutoData]
        public void UserModel_To_DbUser_Mapping_Should_Work_Correctly(UserModel userModel)
        {
            var dbUser = _mapper.Map<DbUser>(userModel);
            Assert.AreEqual(userModel.Id, dbUser.Id);
            Assert.AreEqual(userModel.Created, dbUser.Created);
            Assert.AreEqual(userModel.Updated, dbUser.Updated);
            Assert.AreEqual(userModel.ApiKey, dbUser.ApiKey);
            Assert.AreEqual(userModel.Email, dbUser.Email);
            Assert.AreEqual(userModel.PasswordHash, dbUser.PasswordHash);
            Assert.AreEqual(userModel.Salt, dbUser.Salt);
            Assert.AreEqual(userModel.Username, dbUser.Username);
        }

        [Test, AutoData]
        public void RegisterUserRequestDto_To_UserModel_Mapping_Should_Work_Correctly(RegisterUserRequestDto registerUserRequestDto)
        {
            var userModel = _mapper.Map<UserModel>(registerUserRequestDto);
            Assert.IsTrue(Guid.TryParse(userModel.Id.ToString(), out _) && !userModel.Id.Equals(Guid.Empty));
            Assert.That(userModel.Created, Is.EqualTo(DateTime.UtcNow).Within(1).Seconds);
            Assert.That(userModel.Updated, Is.EqualTo(DateTime.UtcNow).Within(1).Seconds);
            Assert.AreEqual(registerUserRequestDto.Email, userModel.Email);
            Assert.AreEqual(registerUserRequestDto.Username, userModel.Username);
        }

        [Test, AutoData]
        public void UserModel_To_RegisterUserResponseDto_Mapping_Should_Work_Correctly(UserModel userModel)
        {
            var registerUserResponseDto = _mapper.Map<RegisterUserResponseDto>(userModel);
            Assert.AreEqual(userModel.Username, registerUserResponseDto.Username);
        }

        [Test, AutoData]
        public void UserModel_To_LoginUserResponseDto_Mapping_Should_Work_Correctly(UserModel userModel)
        {
            var loginUserResponseDto = _mapper.Map<LoginUserResponseDto>(userModel);
            Assert.AreEqual(userModel.Username, loginUserResponseDto.Username);
        }

        [Test, AutoData]
        public void RegisterUserRequestDto_To_UserModel_Mapping_Should_Generate_New_Fields(RegisterUserRequestDto registerUserRequestDto1,
            RegisterUserRequestDto registerUserRequestDto2)
        {
            var userModel1 = _mapper.Map<UserModel>(registerUserRequestDto1);
            var userModel2 = _mapper.Map<UserModel>(registerUserRequestDto2);
            Assert.AreNotEqual(userModel1.Id, userModel2.Id);
            Assert.AreNotEqual(userModel1.Created, userModel2.Created);
            Assert.AreNotEqual(userModel1.Updated, userModel2.Updated);
        }
    }
}
