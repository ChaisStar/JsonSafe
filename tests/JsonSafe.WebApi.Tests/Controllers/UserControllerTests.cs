namespace JsonSafe.WebApi.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoFixture.NUnit3;
    using AutoMapper;
    using Dtos.UserModels;
    using JsonSafe.Infrastructure.Exceptions.BusinessLogicExceptions;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using NUnit.Framework;
    using Services;
    using Services.Infrastructure;
    using WebApi.Controllers;

    [TestFixture]
    public class UserControllerTests
    {
        private IUserService _userService;
        private IJwtTokenService _jwtTokenService;
        private IMapper _mapper;
        private UserController _userControllerInstance;
        
        [SetUp]
        public void Setup()
        {
            _userService = Substitute.For<IUserService>();
            _jwtTokenService = Substitute.For<IJwtTokenService>();
            _mapper = Substitute.For<IMapper>();
            _userControllerInstance = new UserController(_userService, _jwtTokenService, _mapper);
        }

        [Test, AutoData]
        public async Task RegisterUserAsync_Should_Return_Correct_Result_If_No_Exceptions(RegisterUserRequestDto registerUserRequestDto,
            UserModel userModel, string token, RegisterUserResponseDto registerUserResponseDto)
        {
            _userService.CreateAsync(registerUserRequestDto.Username, registerUserRequestDto.Password,
                registerUserRequestDto.Email).Returns(userModel);
            _mapper.Map<RegisterUserResponseDto>(userModel).Returns(registerUserResponseDto);

            _jwtTokenService.CreateToken(userModel).Returns(token);

            var result = await _userControllerInstance.RegisterUserAsync(registerUserRequestDto);

            var response = result as OkObjectResult;
            Assert.NotNull(response);
            var responseObject = response.Value as RegisterUserResponseDto;
            Assert.NotNull(responseObject);

            Assert.AreEqual(registerUserResponseDto.Username, responseObject.Username);
            Assert.AreEqual(token, responseObject.Token);
        }

        [Test, AutoData]
        public async Task RegisterUserAsync_Should_Return_NotFound_With_Message_If_BaseBusinessLogicException(
            RegisterUserRequestDto registerUserRequestDto,
            UsernameExistException usernameExistException)
        {
            _userService.CreateAsync(registerUserRequestDto.Username, registerUserRequestDto.Password,
                registerUserRequestDto.Email).Throws(usernameExistException);

            var result = await _userControllerInstance.RegisterUserAsync(registerUserRequestDto);

            var response = result as NotFoundObjectResult;
            Assert.NotNull(response);
            var responseObject = response.Value as string;
            Assert.NotNull(responseObject);

            Assert.AreEqual(usernameExistException.Message, responseObject);
        }

        [Test, AutoData]
        public async Task RegisterUserAsync_Should_Return_InternalServerError_With_Message_If_Exception(
            RegisterUserRequestDto registerUserRequestDto,
            Exception exception)
        {
            _userService.CreateAsync(registerUserRequestDto.Username, registerUserRequestDto.Password,
                registerUserRequestDto.Email).Throws(exception);

            var result = await _userControllerInstance.RegisterUserAsync(registerUserRequestDto);

            var response = result as ObjectResult;
            Assert.NotNull(response);
            Assert.AreEqual(500, response.StatusCode);
            var responseObject = response.Value as string;
            Assert.NotNull(responseObject);

            Assert.AreEqual(exception.Message, responseObject);
        }

        [Test, AutoData]
        public async Task LoginUserAsync_Should_Return_Correct_Result_If_No_Exceptions(LoginUserRequestDto loginUserRequestDto,
            UserModel userModel, string token, LoginUserResponseDto loginUserResponseDto)
        {
            _userService.GetAsync(loginUserRequestDto.Username, loginUserRequestDto.Password).Returns(userModel);
            _mapper.Map<LoginUserResponseDto>(userModel).Returns(loginUserResponseDto);

            _jwtTokenService.CreateToken(userModel).Returns(token);

            var result = await _userControllerInstance.LoginUserAsync(loginUserRequestDto);

            var response = result as OkObjectResult;
            Assert.NotNull(response);
            var responseObject = response.Value as LoginUserResponseDto;
            Assert.NotNull(responseObject);

            Assert.AreEqual(loginUserResponseDto.Username, responseObject.Username);
            Assert.AreEqual(token, responseObject.Token);
        }

        [Test, AutoData]
        public async Task LoginUserAsync_Should_Return_NotFound_With_Message_If_BaseBusinessLogicException(
            LoginUserRequestDto loginUserRequestDto,
            UsernameExistException usernameExistException)
        {
            _userService.GetAsync(loginUserRequestDto.Username, loginUserRequestDto.Password).Throws(usernameExistException);

            var result = await _userControllerInstance.LoginUserAsync(loginUserRequestDto);

            var response = result as NotFoundObjectResult;
            Assert.NotNull(response);
            var responseObject = response.Value as string;
            Assert.NotNull(responseObject);

            Assert.AreEqual(usernameExistException.Message, responseObject);
        }

        [Test, AutoData]
        public async Task LoginUserAsync_Should_Return_InternalServerError_With_Message_If_Exception(
            LoginUserRequestDto loginUserRequestDto,
            Exception exception)
        {
            _userService.GetAsync(loginUserRequestDto.Username, loginUserRequestDto.Password).Throws(exception);

            var result = await _userControllerInstance.LoginUserAsync(loginUserRequestDto);

            var response = result as ObjectResult;
            Assert.NotNull(response);
            Assert.AreEqual(500, response.StatusCode);
            var responseObject = response.Value as string;
            Assert.NotNull(responseObject);

            Assert.AreEqual(exception.Message, responseObject);
        }
    }
}
