namespace ApplicationInsightsDemo.BusinessLogic.Models.Authentication
{
    public class LoginResultModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsPasswordValid { get; set; }
    }
}