namespace ApplicationInsightsDemo.WebApi.Dtos
{
    public class ErrorDetailsDto
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}