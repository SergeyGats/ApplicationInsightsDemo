using ApplicationInsightsDemo.BusinessLogic.Helpers.Interfaces;
using ApplicationInsightsDemo.BusinessLogic.Models.Authentication;
using ApplicationInsightsDemo.BusinessLogic.Services.Interfaces;

namespace ApplicationInsightsDemo.BusinessLogic.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserService _userService;

        public AuthenticationService(IPasswordHelper passwordHelper, IUserService userService)
        {
            _passwordHelper = passwordHelper;
            _userService = userService;
        }

        public async Task<LoginResultModel> LoginAsync(LoginModel loginModel)
        {
            var userLoginInfo = await _userService.GetUserLoginInfoAsync(loginModel.Email);

            var isPasswordValid = _passwordHelper
                .CheckIsValidPassword(loginModel.Password, userLoginInfo.PasswordHash, userLoginInfo.PasswordSalt);

            var loginResult = new LoginResultModel
            {
                UserId = userLoginInfo.Id,
                Email = userLoginInfo.Email,
                IsPasswordValid = isPasswordValid
            };

            return loginResult;
        }
    }
}