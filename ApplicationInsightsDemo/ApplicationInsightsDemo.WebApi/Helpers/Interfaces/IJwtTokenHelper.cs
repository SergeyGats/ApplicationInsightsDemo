namespace ApplicationInsightsDemo.WebApi.Helpers.Interfaces
{
    public interface IJwtTokenHelper
    {
        string GenerateJwtToken(int userId);
    }
}