using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers.Options;
using Identity.Services.Interfaces.Models.Jwt;
using Identity.Services.Interfaces.Models.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

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
                    //new Claim(Identity.Services.Interfaces.Models.Jwt.JwtRegisteredClaimNames.Id.ToString(), user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    new Claim(Claims.Scope.ToString().ToLower(), user.Role.ToString()),
                    //new Claim(Identity.Services.Interfaces.Models.Jwt.JwtRegisteredClaimNames.FirstName.ToString().ToLower(), user.FirstName ?? string.Empty),
                    //new Claim(Identity.Services.Interfaces.Models.Jwt.JwtRegisteredClaimNames.LastName.ToString().ToLower(), user.LastName ?? string.Empty),
                    //new Claim(Identity.Services.Interfaces.Models.Jwt.JwtRegisteredClaimNames.Login.ToString().ToLower(), user.Login ?? string.Empty),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(authToken);

            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out SecurityToken validatedToken);
            return true;
        }

        private TokenValidationParameters GetValidationParameters(string key)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // The same key as the one that generate the token
            };
        }
    }
}