using System.ComponentModel.DataAnnotations;

namespace Identity.Services.Interfaces.Models
{
    public class RegisterCoreModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}