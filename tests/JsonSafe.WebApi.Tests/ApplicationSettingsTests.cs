namespace JsonSafe.WebApi.Tests
{
    using System.Collections.Generic;
    using ApplicationSettings;
    using AutoFixture.NUnit3;
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;

    [TestFixture]
    public class ApplicationSettingsTests
    {
        [Test, AutoData]
        public void Mapping_Should_Work_Correctly(string key,
            string issuer,
            int lifetime,
            string name,
            string username,
            string password,
            string host,
            int port,
            string collectionName
            )
        {
            var dictionary = new Dictionary<string, string>
            {
                {"jwtToken:key", key},
                {"jwtToken:issuer", issuer},
                {"jwtToken:lifetime", lifetime.ToString()},
                {"database:name", name},
                {"database:username", username},
                {"database:password", password},
                {"database:host", host},
                {"database:port", port.ToString()},
                {"database:collectionName", collectionName}
            };

            var confBuilder = new ConfigurationBuilder();
            confBuilder.AddInMemoryCollection(dictionary);

            var appSettings = new AppSettings(confBuilder.Build());

            Assert.AreEqual(key, appSettings.JwtToken.Key);
            Assert.AreEqual(issuer, appSettings.JwtToken.Issuer);
            Assert.AreEqual(lifetime, appSettings.JwtToken.Lifetime);
            Assert.AreEqual(name, appSettings.Database.Name);
            Assert.AreEqual(username, appSettings.Database.Username);
            Assert.AreEqual(password, appSettings.Database.Password);
            Assert.AreEqual(host, appSettings.Database.Host);
            Assert.AreEqual(port, appSettings.Database.Port);
            Assert.AreEqual(collectionName, appSettings.Database.CollectionName);
        }
    }
}