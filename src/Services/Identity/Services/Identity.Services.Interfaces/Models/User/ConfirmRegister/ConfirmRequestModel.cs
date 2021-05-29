namespace Identity.Services.Interfaces.Models.User.ConfirmRegister
{
    public class ConfirmRequestModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
