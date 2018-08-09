namespace JsonSafe.Dtos.Tests.FluentValidators
{
    using System.Linq;
    using Dtos.FluentValidators;
    using FluentValidation.Results;
    using NUnit.Framework;
    using UserModels;

    [TestFixture]
    public class RegisterUserRequestDtoValidatorTests
    {
        [Test]
        [TestCase("test@gmail.com", "username", "password", "password", ExpectedResult = true)]
        [TestCase("testgmail.com", "username", "password", "password", ExpectedResult = false)]
        [TestCase("test@gmail.com", "user", "password", "password", ExpectedResult = false)]
        [TestCase("test@gmail.com", "usernameusernameusernameusernameusernameusernameusernameusernameusernameusernameusernameusernameusernameusernameusernameusername",
            "password", "password", ExpectedResult = false)]
        [TestCase("test@gmail.com", "username", "pass", "pass", ExpectedResult = false)]
        [TestCase("test@gmail.com", "username", "password", "password1", ExpectedResult = false)]
        public bool Validator_Should_Work_Correctly(string email, string username, string password,
            string confirmPassword)
        {
            return new RegisterUserRequestDtoValidator().Validate(new RegisterUserRequestDto
            {
                Username = username,
                Password = password,
                Email = email,
                ConfirmPassword = confirmPassword
            }).IsValid;
        }
    }
}
