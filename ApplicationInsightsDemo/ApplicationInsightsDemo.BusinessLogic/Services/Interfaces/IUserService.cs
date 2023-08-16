using ApplicationInsightsDemo.BusinessLogic.Models.User;

namespace ApplicationInsightsDemo.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(int id);
        Task<UserLoginInfoModel> GetUserLoginInfoAsync(string email);
        Task CreateAsync(UserCreateModel createModel);
        Task UpdateAsync(UserUpdateModel updateModel);
        Task DeleteAsync(int id);
    }
}