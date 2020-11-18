using System.ComponentModel.DataAnnotations;

namespace Identity.Services.Interfaces.Models.User.Register
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "Email not specified")]
        //[EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Incorrect password")]
        public string ConfirmPassword { get; set; }
    }
}