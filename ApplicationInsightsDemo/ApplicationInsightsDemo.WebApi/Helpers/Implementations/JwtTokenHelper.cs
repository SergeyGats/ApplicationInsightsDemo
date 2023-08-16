using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApplicationInsightsDemo.Configuration.Models;
using ApplicationInsightsDemo.WebApi.Constants;
using ApplicationInsightsDemo.WebApi.Helpers.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApplicationInsightsDemo.WebApi.Helpers.Implementations
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly IOptions<JwtAuthenticationSettings> _jwtAuthenticationSettings;

        public JwtTokenHelper(IOptions<JwtAuthenticationSettings> jwtAuthenticationSettings)
        {
            _jwtAuthenticationSettings = jwtAuthenticationSettings;
        }

        public string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtAuthenticationSettings.Value.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateClaimsIdentity(userId),
                Expires = DateTime.UtcNow.Date.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(securityToken);

            return jwtToken;
        }

        private ClaimsIdentity CreateClaimsIdentity(int userId)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypeConstants.UserId, userId.ToString())
        };

            var claimIdentity = new ClaimsIdentity(claims);
            return claimIdentity;
        }
    }
}