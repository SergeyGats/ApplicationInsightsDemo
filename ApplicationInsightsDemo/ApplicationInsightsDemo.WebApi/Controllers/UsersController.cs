using ApplicationInsightsDemo.BusinessLogic.Models.User;
using ApplicationInsightsDemo.BusinessLogic.Services.Interfaces;
using ApplicationInsightsDemo.WebApi.Dtos.User;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationInsightsDemo.WebApi.Controllers
{
    public class UsersController : ApplicationControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger,
            IMapper mapper,
            IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var utcNow = DateTime.UtcNow;
            _logger.LogTrace($"LogTrace {utcNow}");
            _logger.LogInformation($"LogInformation {utcNow}");
            _logger.LogError($"LogError {utcNow}");
            _logger.LogWarning($"LogWarning {utcNow}");

            var userModels = await _userService.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(userModels);

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var userModel = await _userService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(userModel);

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserCreateDto createDto)
        {
            var createModel = _mapper.Map<UserCreateModel>(createDto);
            await _userService.CreateAsync(createModel);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UserUpdateDto updateDto)
        {
            var updateModel = _mapper.Map<UserUpdateModel>(updateDto);
            await _userService.UpdateAsync(updateModel);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userService.DeleteAsync(id);

            return Ok();
        }
    }
}