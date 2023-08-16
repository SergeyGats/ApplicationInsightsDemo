namespace ApplicationInsightsDemo.DataAccess.DataModels
{
    public class UserLoginInfoDataModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}