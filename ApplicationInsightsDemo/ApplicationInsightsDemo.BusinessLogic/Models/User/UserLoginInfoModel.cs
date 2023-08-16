namespace ApplicationInsightsDemo.BusinessLogic.Models.User
{
    public class UserLoginInfoModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}