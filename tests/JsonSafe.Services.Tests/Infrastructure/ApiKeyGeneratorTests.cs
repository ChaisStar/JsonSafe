namespace JsonSafe.Services.Tests.Infrastructure
{
    using System;
    using AutoFixture.NUnit3;
    using NSubstitute;
    using NUnit.Framework;
    using Services.Infrastructure;

    [TestFixture]
    public class ApiKeyGeneratorTests
    {
        [Test, AutoData, Repeat(10)]
        public void GenerateNewApiKey_Should_Convert_NumerGeneratorString(byte[] bytes)
        {
            var numberGenerator = Substitute.For<INumberGenerator>();
            numberGenerator.GetBytes().Returns(bytes);
            var apiKeyGenerator = new ApiKeyGenerator(numberGenerator);
            Assert.AreEqual(Convert.ToBase64String(bytes), apiKeyGenerator.GenerateNewApiKey());
        }
    }
}
