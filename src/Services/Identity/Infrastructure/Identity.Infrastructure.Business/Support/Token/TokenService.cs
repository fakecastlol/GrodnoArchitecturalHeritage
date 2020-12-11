using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers.Options;
using Identity.Services.Interfaces.Models.Jwt;
using Identity.Services.Interfaces.Models.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Business.Support.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateJwtToken(UserResponseCoreModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(Claims.Id.ToString().ToLower(), user.Id.ToString()),
                    new Claim(Claims.Email.ToString().ToLower(), user.Email),
                    new Claim(Claims.Scope.ToString().ToLower(), user.Role.ToString()),
                    new Claim(Claims.FirstName.ToString().ToLower(), user.FirstName ?? string.Empty),
                    new Claim(Claims.LastName.ToString().ToLower(), user.LastName ?? string.Empty),
                    new Claim(Claims.Login.ToString().ToLower(), user.Login ?? string.Empty),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}