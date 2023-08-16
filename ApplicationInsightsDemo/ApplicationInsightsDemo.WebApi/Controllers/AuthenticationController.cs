using ApplicationInsightsDemo.BusinessLogic.Models.Authentication;
using ApplicationInsightsDemo.BusinessLogic.Services.Interfaces;
using ApplicationInsightsDemo.WebApi.Dtos.Authentication;
using ApplicationInsightsDemo.WebApi.Helpers.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationInsightsDemo.WebApi.Controllers
{
    public class AuthenticationController : ApplicationControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJwtTokenHelper _jwtTokenHelper;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IMapper mapper,
            IJwtTokenHelper jwtTokenHelper,
            IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _jwtTokenHelper = jwtTokenHelper;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var loginModel = _mapper.Map<LoginModel>(loginDto);
            var loginResultModel = await _authenticationService.LoginAsync(loginModel);
            var loginResultDto = _mapper.Map<LoginResultDto>(loginResultModel);

            if (loginResultDto.IsPasswordValid)
            {
                loginResultDto.JwtToken = _jwtTokenHelper.GenerateJwtToken(loginResultDto.UserId);
            }

            return Ok(loginResultDto);
        }
    }
}
