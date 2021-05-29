using System.ComponentModel.DataAnnotations;

namespace Identity.Services.Interfaces.Models.User.Register
{
    public class RegisterRequestModel
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
        //[RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$",
        //    ErrorMessage = "Password must meet requirements")]
        public string Password { get; set; }
    }
}