namespace JsonSafe.Database.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoFixture.NUnit3;
    using AutoMapper;
    using DbModels;
    using Models;
    using Neleus.LambdaCompare;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class JsonRepositoryTests
    {
        private IMongoCollectionClient<DbJson> _mongoCollectionClient;
        private IMapper _mapper;
        private JsonRepository _jsonRepositoryInstance;

        [SetUp]
        public void Setup()
        {
            _mongoCollectionClient = Substitute.For<IMongoCollectionClient<DbJson>>();
            _mapper = Substitute.For<IMapper>();
            _jsonRepositoryInstance = new JsonRepository(_mongoCollectionClient, _mapper);
        }

        [Test, AutoData]
        public async Task AddJsonAsync_Should_Add_One_JsonAsync(string username, DbJson dbJson, JsonModel jsonModel)
        {
            _mapper.Map<DbJson>(jsonModel).Returns(dbJson);
            await _jsonRepositoryInstance.AddJsonAsync(username, jsonModel);
            await _mongoCollectionClient.Received(1).InsertOneAsync(username, dbJson);
        }

        [Test, AutoData]
        public async Task GetUserJsonsAsync_Should_Return_All_User_Jsons(string username, Dictionary<DbJson, JsonModel> jsonModelsMapping)
        {
            Expression<Func<DbJson, bool>> expression = null;
            _mongoCollectionClient
                .When(x => x.FindAsync(username, Arg.Any<Expression<Func<DbJson, bool>>>()))
                .Do(info => { expression = (Expression<Func<DbJson, bool>>)info[1]; });
            _mongoCollectionClient
                .FindAsync(username, Arg.Any<Expression<Func<DbJson, bool>>>())
                .Returns(jsonModelsMapping.Keys);
            _mapper.Map<JsonModel>(Arg.Any<DbJson>())
                .Returns(x => jsonModelsMapping.FirstOrDefault(j => ((DbJson) x[0]).Id == j.Key.Id).Value);

            var result = await _jsonRepositoryInstance.GetUserJsonsAsync(username);
            Expression<Func<DbJson, bool>> expectedExpression = x => true;
            Assert.AreEqual(jsonModelsMapping.Values, result);
            Assert.IsTrue(Lambda.Eq(expectedExpression, expression));
        }

        [Test, AutoData]
        public async Task GetJsonAsync_Should_Return_User_Json_With_Same_IdAsync(string username, DbJson dbJson, JsonModel jsonModel)
        {
            Expression<Func<DbJson, bool>> expression = null;
            _mongoCollectionClient
                .When(x => x.FindOneAsync(username, Arg.Any<Expression<Func<DbJson, bool>>>()))
                .Do(info => { expression = (Expression<Func<DbJson, bool>>)info[1]; });
            _mongoCollectionClient
                .FindOneAsync(username, Arg.Any<Expression<Func<DbJson, bool>>>())
                .Returns(dbJson);
            _mapper.Map<JsonModel>(dbJson).Returns(jsonModel);

            var result = await _jsonRepositoryInstance.GetJsonAsync(username, dbJson.Id);
            Expression<Func<DbJson, bool>> expectedExpression = x => x.Id == dbJson.Id;
            Assert.AreEqual(jsonModel, result);
            Assert.IsTrue(Lambda.Eq(expectedExpression, expression));
        }

        [Test, AutoData]
        public async Task DeleteJsonAsync_Should_Delete_User_Json_With_Same_IdAsync(string username, Guid jsonId)
        {
            Expression<Func<DbJson, bool>> expression = null;
            _mongoCollectionClient
                .When(x => x.DeleteOneAsync(username, Arg.Any<Expression<Func<DbJson, bool>>>()))
                .Do(info => { expression = (Expression<Func<DbJson, bool>>)info[1]; });

            await _jsonRepositoryInstance.DeleteJsonAsync(username, jsonId);
            Expression<Func<DbJson, bool>> expectedExpression = x => x.Id == jsonId;
            Assert.IsTrue(Lambda.Eq(expectedExpression, expression));
        }
    }
}
