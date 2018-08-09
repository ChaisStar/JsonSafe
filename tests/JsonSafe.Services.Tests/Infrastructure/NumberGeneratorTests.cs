namespace JsonSafe.Services.Tests.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture.NUnit3;
    using NUnit.Framework;
    using Services.Infrastructure;

    [TestFixture]
    public class NumberGeneratorTests
    {
        [Test, AutoData]
        public void GetBytes_Should_Return_Random_Results(int iterations)
        {
            var numberGenerator = new NumberGenerator();
            var results = new List<byte[]>();
            for (var i = 0; i < iterations; i++)
            {
                results.Add(numberGenerator.GetBytes());
            }

            Assert.AreEqual(iterations, results.Distinct().Count());
        }
    }
}
