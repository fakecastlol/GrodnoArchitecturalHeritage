namespace Identity.Services.Interfaces.Models
{
    public class RegisterResponseModel
    {
        public UserResponseCoreModel User { get; set; }

        public string Token { get; set; }
    }
}
