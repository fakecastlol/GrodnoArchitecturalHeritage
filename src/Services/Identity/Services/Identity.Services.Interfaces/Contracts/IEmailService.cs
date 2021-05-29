using System.Threading.Tasks;

namespace Identity.Services.Interfaces.Contracts
{
    public interface IEmailService
    {
        Task SendConfirmEmailAsync(string email, string subject, string message);
        Task SendForgotPasswordAsync(string email, string subject, string message);
    }
}
