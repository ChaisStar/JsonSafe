namespace JsonSafe.Mappings.Tests
{
    using System;
    using AutoFixture.NUnit3;
    using AutoMapper;
    using DbModels;
    using Dtos.JsonModels;
    using Models;
    using NUnit.Framework;

    [TestFixture]
    public class JsonProfileTests
    {
        private readonly IMapper _mapper;

        public JsonProfileTests()
        {
            var mapperConfiguration = new MapperConfiguration(x => x.AddProfile<JsonProfile>());
            mapperConfiguration.AssertConfigurationIsValid();
            _mapper = new Mapper(mapperConfiguration);
        }

        [Test, AutoData]
        public void DbJson_To_JsonModel_Mapping_Should_Work_Correctly(DbJson dbJson)
        {
            var jsonModel = _mapper.Map<JsonModel>(dbJson);
            Assert.AreEqual(dbJson.Id, jsonModel.Id);
            Assert.AreEqual(dbJson.Created, jsonModel.Created);
            Assert.AreEqual(dbJson.Updated, jsonModel.Updated);
            Assert.AreEqual(dbJson.Name, jsonModel.Name);
            Assert.AreEqual(dbJson.Json, jsonModel.Json);
        }

        [Test, AutoData]
        public void JsonModel_To_DbJson_Mapping_Should_Work_Correctly(JsonModel jsonModel)
        {
            var dbJson = _mapper.Map<DbJson>(jsonModel);
            Assert.AreEqual(jsonModel.Id, dbJson.Id);
            Assert.AreEqual(jsonModel.Created, dbJson.Created);
            Assert.AreEqual(jsonModel.Updated, dbJson.Updated);
            Assert.AreEqual(jsonModel.Name, dbJson.Name);
            Assert.AreEqual(jsonModel.Json, dbJson.Json);
        }

        [Test, AutoData]
        public void CreateJsonRequestDto_To_JsonModel_Mapping_Should_Work_Correctly(CreateJsonRequestDto createJsonRequestDto)
        {
            var jsonModel = _mapper.Map<JsonModel>(createJsonRequestDto);
            Assert.IsTrue(Guid.TryParse(jsonModel.Id.ToString(), out _) && !jsonModel.Id.Equals(Guid.Empty));
            Assert.That(jsonModel.Created, Is.EqualTo(DateTime.UtcNow).Within(2).Seconds);
            Assert.That(jsonModel.Updated, Is.EqualTo(DateTime.UtcNow).Within(2).Seconds);
            Assert.AreEqual(createJsonRequestDto.Name, jsonModel.Name);
            Assert.AreEqual(createJsonRequestDto.Json, jsonModel.Json);
        }

        [Test, AutoData]
        public void JsonModel_To_GetJsonResponseDto_Mapping_Should_Work_Correctly(JsonModel jsonModel)
        {
            var getJsonResponseDto = _mapper.Map<GetJsonResponseDto>(jsonModel);
            Assert.AreEqual(jsonModel.Id, getJsonResponseDto.Id);
            Assert.AreEqual(jsonModel.Created, getJsonResponseDto.Created);
            Assert.AreEqual(jsonModel.Updated, getJsonResponseDto.Updated);
            Assert.AreEqual(jsonModel.Name, getJsonResponseDto.Name);
            Assert.AreEqual(jsonModel.Json, getJsonResponseDto.Json);
        }

        [Test, AutoData]
        public void CreateJsonRequestDto_To_JsonModel_Mapping_Should_Generate_New_Fields(CreateJsonRequestDto createJsonRequestDto1, 
            CreateJsonRequestDto createJsonRequestDto2)
        {
            var jsonModel1 = _mapper.Map<JsonModel>(createJsonRequestDto1);
            var jsonModel2 = _mapper.Map<JsonModel>(createJsonRequestDto2);
            Assert.AreNotEqual(jsonModel1.Id, jsonModel2.Id);
            Assert.AreNotEqual(jsonModel1.Created, jsonModel2.Created);
            Assert.AreNotEqual(jsonModel1.Updated, jsonModel2.Updated);
        }
    }
}
