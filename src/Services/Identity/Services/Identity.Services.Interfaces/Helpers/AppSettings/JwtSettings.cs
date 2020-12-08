using Identity.Services.Interfaces.Contracts;

namespace Identity.Services.Interfaces.Helpers.AppSettings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}