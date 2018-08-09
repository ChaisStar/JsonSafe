namespace JsonSafe.Services.Tests.Infrastructure
{
    using AutoFixture.NUnit3;
    using NUnit.Framework;
    using Services.Infrastructure;

    [TestFixture]
    public class PasswordManagerTests
    {
        [Test, AutoData]
        public void PasswordManager_Should_Work_Correctly(string password, string incorrectPassword)
        {
            var passwordManager = new PasswordManager();
            var hashSalt = passwordManager.GeneratePassword(password);
            Assert.AreNotEqual(password, hashSalt.PasswordHash);
            Assert.AreNotEqual(password, hashSalt.Salt);
            var isCorrect = passwordManager.IsPasswordCorrect(password, hashSalt.PasswordHash, hashSalt.Salt);
            Assert.IsTrue(isCorrect);
            isCorrect = passwordManager.IsPasswordCorrect(incorrectPassword, hashSalt.PasswordHash, hashSalt.Salt);
            Assert.IsFalse(isCorrect);
        }
    }
}
