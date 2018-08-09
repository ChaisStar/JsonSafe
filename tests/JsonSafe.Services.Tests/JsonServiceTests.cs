namespace JsonSafe.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoFixture.NUnit3;
    using AutoMapper;
    using Database;
    using DbModels;
    using Models;
    using Newtonsoft.Json;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class JsonServiceTests
    {
        private IJsonRepository _jsonRepository;
        private JsonService _jsonServiceInstance;

        [SetUp]
        public void Setup()
        {
            _jsonRepository = Substitute.For<IJsonRepository>();
            _jsonServiceInstance = new JsonService(_jsonRepository);
        }
        
        [Test, AutoData]
        public async Task CreateAsync_Should_Add_Json_To_User_And_Return_True(string username, JsonModel jsonModel)
        {
            var jsonModelRepository = default(JsonModel);
            _jsonRepository
                .When(x => x.AddJsonAsync(username, Arg.Any<JsonModel>()))
                .Do(x => jsonModelRepository = (JsonModel) x[1]);

            var result = await _jsonServiceInstance.CreateAsync(username, jsonModel);
            Assert.IsTrue(result);
            Assert.AreEqual(jsonModel, jsonModelRepository);
        }

        [Test, AutoData]
        public async Task GetDataAsync_Should_Get_User_Data_With_Correct_IdAsync(string username, Guid id, JsonModel jsonModel)
        {
            _jsonRepository.GetJsonAsync(username, id).Returns(jsonModel);
            var result = await _jsonServiceInstance.GetDataAsync(username, id);
            Assert.AreEqual(jsonModel, result);
        }

        [Test, AutoData]
        public async Task GetAllDataAsync_Should_Get_All_User_DataAsync(string username, List<JsonModel> jsonModels)
        {
            _jsonRepository.GetUserJsonsAsync(username).Returns(jsonModels);
            var result = await _jsonServiceInstance.GetAllDataAsync(username);
            Assert.AreEqual(jsonModels, result);
        }

        [Test, AutoData]
        public async Task DeleteDataAsync_Should_Delete_User_Data_With_Correct_IdAsync(string username, Guid id)
        {
            await _jsonServiceInstance.DeleteDataAsync(username, id);
            await _jsonRepository.Received().DeleteJsonAsync(username, id);
        }
    }
}