using Identity.Services.Interfaces.Models.User.Register;

namespace Identity.Services.Interfaces.Models.User.Register
{
    public class RegisterResponseModel
    {
        public UserResponseCoreModel User { get; set; }

        public string Token { get; set; }
    }
}
