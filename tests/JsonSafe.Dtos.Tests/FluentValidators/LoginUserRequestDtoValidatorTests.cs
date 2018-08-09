namespace JsonSafe.Dtos.Tests.FluentValidators
{
    using Dtos.FluentValidators;
    using NUnit.Framework;
    using UserModels;

    [TestFixture]
    public class LoginUserRequestDtoValidatorTests
    {
        [Test]
        [TestCase("username", "password", ExpectedResult = true)]
        [TestCase("", "password", ExpectedResult = false)]
        [TestCase("username", "", ExpectedResult = false)]
        public bool Validator_Should_Work_Correctly(string username, string password)
        {
            return new LoginUserRequestDtoValidator().Validate(new LoginUserRequestDto()
            {
                Username = username,
                Password = password,
            }).IsValid;
        }
    }
}
