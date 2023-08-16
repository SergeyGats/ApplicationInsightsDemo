namespace ApplicationInsightsDemo.BusinessLogic.Helpers.Interfaces
{
    public interface IPasswordHelper
    {
        byte[] GeneratePasswordHash(string password, string salt);
        bool CheckIsValidPassword(string providedPassword, byte[] passwordHash, string salt);
    }
}