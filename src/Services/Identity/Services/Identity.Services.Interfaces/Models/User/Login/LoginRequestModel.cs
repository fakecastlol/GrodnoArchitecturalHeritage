using System.ComponentModel.DataAnnotations;

namespace Identity.Services.Interfaces.Models.User.Login
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email not specified")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, MinimumLength = 5,
            ErrorMessage = "Email should be minimum 5 characters and a maximum of 20 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 6,
        ErrorMessage = "Email should be minimum 6 characters and a maximum of 40 characters")]
        public string Password { get; set; }
    }
}
