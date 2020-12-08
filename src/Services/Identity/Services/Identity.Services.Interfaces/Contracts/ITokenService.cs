using Identity.Services.Interfaces.Models.User;

namespace Identity.Services.Interfaces.Contracts
{
    public interface ITokenService
    {
        string GenerateJwtToken(UserResponseCoreModel user);
    }
}