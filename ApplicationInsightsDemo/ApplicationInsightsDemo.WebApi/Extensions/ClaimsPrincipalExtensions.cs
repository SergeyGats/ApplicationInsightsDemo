using System.Security.Authentication;
using System.Security.Claims;
using ApplicationInsightsDemo.WebApi.Constants;

namespace ApplicationInsightsDemo.WebApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userIdValue = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypeConstants.UserId)?.Value;

            return !string.IsNullOrEmpty(userIdValue) && int.TryParse(userIdValue, out var userId)
                ? userId
                : throw new AuthenticationException("Invalid user id value.");
        }
    }
}