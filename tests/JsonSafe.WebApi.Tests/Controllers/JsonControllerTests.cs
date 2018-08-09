namespace JsonSafe.WebApi.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using AutoFixture.NUnit3;
    using AutoMapper;
    using Dtos.JsonModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Services;
    using WebApi.Controllers;

    [TestFixture]
    public class JsonControllerTests
    {
        private IJsonService _jsonService;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _jsonService = Substitute.For<IJsonService>();
            _mapper = Substitute.For<IMapper>();
        }

        private JsonController CreateJsonController(string username)
        {
            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }, string.Empty))
            };

            return new JsonController(_jsonService, _mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };
        }

        [Test, AutoData]
        public async Task GetAllJsonsAsync_Should_Work_Correctly(string username, Dictionary<JsonModel, GetJsonResponseDto> jsonModelsDictionary)
        {
            _jsonService.GetAllDataAsync(username).Returns(info => jsonModelsDictionary.Keys);
            _mapper.Map<GetJsonResponseDto>(Arg.Any<JsonModel>())
                .Returns(x => jsonModelsDictionary.FirstOrDefault(d => d.Key.Id.Equals(((JsonModel) x[0]).Id)).Value);


            var response = await CreateJsonController(username).GetAllJsonsAsync();
            var objectResult = response as OkObjectResult;
            Assert.NotNull(objectResult);
            var responseObject = objectResult.Value as IEnumerable<GetJsonResponseDto>;
            Assert.NotNull(responseObject);
            for (var i = 0; i < jsonModelsDictionary.Count; i++)
            {
                var jsonModel = jsonModelsDictionary.Values.ElementAt(i);
                var el = responseObject.ElementAt(i);
                Assert.AreEqual(jsonModel.Json, el.Json);
            }
        }

        [Test, AutoData]
        public async Task GetJsonAsync_Should_Work_Correctly(string username, JsonModel jsonModel, 
            Guid jsonId, GetJsonResponseDto getJsonResponseDto)
        {
            _jsonService.GetDataAsync(username, jsonId).Returns(info => jsonModel);
            _mapper.Map<GetJsonResponseDto>(jsonModel).Returns(getJsonResponseDto);

            var response = await CreateJsonController(username).GetJsonAsync(jsonId);
            var objectResult = response as OkObjectResult;
            Assert.NotNull(objectResult);
            var responseObject = objectResult.Value as GetJsonResponseDto;
            Assert.NotNull(responseObject);
            Assert.AreEqual(getJsonResponseDto, responseObject);
        }

        [Test, AutoData]
        public async Task DeleteJsonAsync_Should_Work_Correctly(string username, Guid jsonId)
        {
            var response = await CreateJsonController(username).DeleteJsonAsync(jsonId);
            await _jsonService.Received().DeleteDataAsync(username, jsonId);
            var result = response as OkResult;
            Assert.NotNull(result);
        }

        [Test, AutoData]
        public async Task CreateJsonAsync_Should_Work_Correctly(string username, JsonModel jsonModel, CreateJsonRequestDto createJsonRequestDto)
        {
            _mapper.Map<JsonModel>(createJsonRequestDto).Returns(jsonModel);
            _jsonService.CreateAsync(username, jsonModel).Returns(true);
            var response = await CreateJsonController(username).CreateJsonAsync(createJsonRequestDto);
            var result = response as OkResult;
            Assert.NotNull(result);
        }

        [Test, AutoData]
        public async Task CreateJsonAsync_Should_Work_Correctly2(string username, JsonModel jsonModel, CreateJsonRequestDto createJsonRequestDto)
        {
            _mapper.Map<JsonModel>(createJsonRequestDto).Returns(jsonModel);
            _jsonService.CreateAsync(username, jsonModel).Returns(false);
            var response = await CreateJsonController(username).CreateJsonAsync(createJsonRequestDto);
            var result = response as BadRequestResult;
            Assert.NotNull(result);
        }
    }
}
