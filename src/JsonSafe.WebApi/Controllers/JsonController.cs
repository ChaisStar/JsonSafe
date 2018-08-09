namespace JsonSafe.WebApi.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Extensions;
    using Dtos.JsonModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    [Route("api/jsons")]
    [Authorize]
    public class JsonController : Controller
    {
        private readonly IJsonService _jsonService;
        private readonly IMapper _mapper;

        public JsonController(IJsonService jsonService, IMapper mapper)
        {
            _jsonService = jsonService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJsonsAsync()
        {
            return Ok((await _jsonService.GetAllDataAsync(User.GetUsername()))
                .Select(x => _mapper.Map<GetJsonResponseDto>(x)));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetJsonAsync([FromRoute]Guid id)
        {
            return Ok(_mapper.Map<GetJsonResponseDto>(await _jsonService.GetDataAsync(User.GetUsername(), id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJsonAsync([FromBody]CreateJsonRequestDto createJsonRequestDto)
        {
            if (await _jsonService.CreateAsync(User.GetUsername(), _mapper.Map<JsonModel>(createJsonRequestDto)))
                return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteJsonAsync([FromRoute]Guid id)
        {
            await _jsonService.DeleteDataAsync(User.GetUsername(), id);
            return Ok();
        }
    }
}
