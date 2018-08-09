namespace JsonSafe.WebApi.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using Dtos.UserModels;
    using JsonSafe.Infrastructure.Exceptions.BusinessLogicExceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Infrastructure;

    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequestDto dto)
        {
            try
            {
                var user = await _userService.CreateAsync(dto.Username, dto.Password, dto.Email);
                var response = _mapper.Map<RegisterUserResponseDto>(user);
                response.Token = _jwtTokenService.CreateToken(user);
                return Ok(response);
            }
            catch (BaseBusinessLogicException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequestDto dto)
        {
            try
            {
                var user = await _userService.GetAsync(dto.Username, dto.Password);
                var response = _mapper.Map<LoginUserResponseDto>(user);
                response.Token = _jwtTokenService.CreateToken(user);
                return Ok(response);
            }
            catch (BaseBusinessLogicException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
