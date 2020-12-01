using Identity.Services.Interfaces.Models.User.Abstract;

namespace Identity.Services.Interfaces.Models.User
{
    public class ProfileRequestModel : CoreModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Location { get; set; }
    }
}
