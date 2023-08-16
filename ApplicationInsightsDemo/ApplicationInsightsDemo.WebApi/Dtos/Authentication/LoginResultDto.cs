namespace ApplicationInsightsDemo.WebApi.Dtos.Authentication
{
    public class LoginResultDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public bool IsPasswordValid { get; set; }
    }
}