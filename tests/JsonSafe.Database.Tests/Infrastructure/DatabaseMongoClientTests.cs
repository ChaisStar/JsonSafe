namespace JsonSafe.Database.Tests.Infrastructure
{
    using AutoFixture.NUnit3;
    using Database.Infrastructure;
    using MongoDB.Driver;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseMongoClientTests
    {
        [Test, AutoData]
        public void MongoClient_Should_Use_Correct_Fields(
            string name,
            string username,
            string password,
            string host,
            int port)
        {
            var databaseConfig = Substitute.For<IDatabaseConfig>();
            databaseConfig.Name.Returns(name);
            databaseConfig.Username.Returns(username);
            databaseConfig.Password.Returns(password);
            databaseConfig.Host.Returns(host);
            databaseConfig.Port.Returns(port);

            var client = new DatabaseMongoClient(databaseConfig).MongoClient;
            Assert.IsNotNull(client);
            Assert.AreEqual(name, client.Settings.Credential.Source);
            Assert.AreEqual(username, client.Settings.Credential.Username);
            Assert.AreEqual(new PasswordEvidence(password), client.Settings.Credential.Evidence);
            Assert.AreEqual(host, client.Settings.Server.Host);
            Assert.AreEqual(port, client.Settings.Server.Port);
        }
    }
}
