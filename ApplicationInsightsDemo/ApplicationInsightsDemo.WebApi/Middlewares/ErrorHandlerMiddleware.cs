using System.Net.Mime;
using ApplicationInsightsDemo.Common.Exceptions;
using ApplicationInsightsDemo.WebApi.Dtos;

namespace ApplicationInsightsDemo.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private const string InternalServerErrorText = "Internal Server Error.";
        private const string EntityNotFoundErrorText = "Entity not found.";

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, string.Empty);

            var response = context.Response;

            var (statusCode, error) = GetStatusCodeAndError(exception);
            response.ContentType = MediaTypeNames.Application.Json;
            response.StatusCode = statusCode;
            var message = exception.GetBaseException().Message;

            var errorDetailsViewModel = new ErrorDetailsDto
            {
                StatusCode = statusCode,
                Error = error,
                Message = message
            };

            return context.Response.WriteAsJsonAsync(errorDetailsViewModel);
        }

        private (int StatusCode, string Error) GetStatusCodeAndError(Exception exception)
        {
            int statusCode;
            string error;

            switch (exception)
            {
                case EntityNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    error = EntityNotFoundErrorText;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    error = InternalServerErrorText;
                    break;
            }

            return (statusCode, error);
        }
    }
}