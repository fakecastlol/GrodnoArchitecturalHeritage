using System.ComponentModel.DataAnnotations;

namespace Identity.Services.Interfaces.Models.User.Login
{
    public class LoginCoreModel
    {
        [Required(ErrorMessage = "Email not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
