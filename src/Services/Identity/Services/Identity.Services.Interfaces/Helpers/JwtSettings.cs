using Identity.Services.Interfaces.Contracts;

namespace Identity.Services.Interfaces.Helpers
{
    public class JwtSettings : IJwtService
    {
        public string Secret { get; set; }
    }
}