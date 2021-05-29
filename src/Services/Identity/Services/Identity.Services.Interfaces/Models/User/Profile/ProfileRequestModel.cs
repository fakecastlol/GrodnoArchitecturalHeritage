using System.ComponentModel.DataAnnotations;
using Identity.Services.Interfaces.Models.User.Register.Abstract;

namespace Identity.Services.Interfaces.Models.User.Profile
{
    public class ProfileRequestModel : CoreModel
    {
        [Required(ErrorMessage = "Email not specified")]
        [EmailAddress]
        [StringLength(30, MinimumLength = 5,
            ErrorMessage = "Last Name should be minimum 5 characters and a maximum of 30 characters")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2,
            ErrorMessage = "Last Name should be minimum 2 characters and a maximum of 20 characters")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2,
            ErrorMessage = "Last Name should be minimum 2 characters and a maximum of 20 characters")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 4,
            ErrorMessage = "Last Name should be minimum 4 characters and a maximum of 20 characters")]
        public string Login { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2,
            ErrorMessage = "Last Name should be minimum 2 characters and a maximum of 20 characters")]
        public string Location { get; set; }
    }
}
