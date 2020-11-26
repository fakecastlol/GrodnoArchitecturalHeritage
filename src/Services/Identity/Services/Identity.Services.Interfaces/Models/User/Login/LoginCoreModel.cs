using System;

namespace Identity.Services.Interfaces.Models.User.Login
{
    public class LoginCoreModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime LastVisited { get; set; }
    }
}
