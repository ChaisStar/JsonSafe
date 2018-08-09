namespace JsonSafe.Dtos.Tests.FluentValidators
{
    using AutoFixture.NUnit3;
    using Dtos.FluentValidators;
    using JsonModels;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using UserModels;

    [TestFixture]
    public class CreateJsonRequestDtoValidatorTests
    {
        [Test, AutoData]
        public void Validator_Should_Not_Return_Error_For_Correct_Json_Object(LoginUserRequestDto requestDto, string name)
        {
            Assert.True(new CreateJsonRequestDtoValidator()
                .Validate(new CreateJsonRequestDto
                {
                    Json = JsonConvert.SerializeObject(requestDto),
                    Name = name
                })
                .IsValid);
        }

        [Test, AutoData]
        public void Validator_Should_Not_Return_Error_For_Correct_Json_Array(LoginUserRequestDto[] requestDtos, string name)
        {
            Assert.True(new CreateJsonRequestDtoValidator()
                .Validate(new CreateJsonRequestDto
                {
                    Json = JsonConvert.SerializeObject(requestDtos),
                    Name = name
                })
                .IsValid);
        }

        [Test, AutoData]
        public void Validator_Should_Return_Error_For_Incorrect_Json(string requestDto, string name)
        {
            Assert.False(new CreateJsonRequestDtoValidator()
                .Validate(new CreateJsonRequestDto
                {
                    Json = JsonConvert.SerializeObject(requestDto),
                    Name = name
                })
                .IsValid);
        }

        [Test, AutoData]
        public void Validator_Should_Return_Error_For_Empty_Json(string name)
        {
            Assert.False(new CreateJsonRequestDtoValidator()
                .Validate(new CreateJsonRequestDto
                {
                    Json = string.Empty,
                    Name = name
                })
                .IsValid);
        }

        [Test, AutoData]
        public void Validator_Should_Return_Error_For_Incorrect_Json(string name)
        {
            Assert.False(new CreateJsonRequestDtoValidator()
                .Validate(new CreateJsonRequestDto
                {
                    Json = "{123}",
                    Name = name
                })
                .IsValid);
        }

        [Test]
        public void Validator_Should_Return_Error_For_Empty_Request()
        {
            Assert.False(new CreateJsonRequestDtoValidator()
                .Validate(default(CreateJsonRequestDto))
                .IsValid);
        }
    }
}
