using ApplicationInsightsDemo.BusinessLogic.Models.Authentication;

namespace ApplicationInsightsDemo.BusinessLogic.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResultModel> LoginAsync(LoginModel loginModel);
    }
}