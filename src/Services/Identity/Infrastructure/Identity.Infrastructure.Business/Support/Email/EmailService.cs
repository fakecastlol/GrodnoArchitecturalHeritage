using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Business.Support.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendConfirmEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                var url = "https://localhost:5002/confirmregister"; 
                emailMessage.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new BodyBuilder(/*.TextFormat.Html*/)
                {
                    HtmlBody = $"<div style=\"color: green;\">To complete registration follow the <a href = {url} target=_blank> link </a></div>"
                }.ToMessageBody();

                using var client = new SmtpClient();
                //{
                //    ServerCertificateValidationCallback = (s, c, h, e) => true
                //};

                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                
                await client.AuthenticateAsync(_smtpSettings.SenderEmail, _smtpSettings.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task SendForgotPasswordAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                var url = "https://localhost:5002/confirmregister";
                emailMessage.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new BodyBuilder(/*.TextFormat.Html*/)
                {
                    HtmlBody = $"<div style=\"color: green;\">Your new password: {message}"
                }.ToMessageBody();

                using var client = new SmtpClient();
                //{
                //    ServerCertificateValidationCallback = (s, c, h, e) => true
                //};

                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);

                await client.AuthenticateAsync(_smtpSettings.SenderEmail, _smtpSettings.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
