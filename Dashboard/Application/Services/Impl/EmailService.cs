using Application.Common.Email;
using MimeKit;
using MailKit.Net.Smtp;
using Serilog;

namespace Application.Services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            await SendAsync(CreateEmail(emailMessage));
        }

        private async Task SendAsync(MimeMessage message)
        {
            using SmtpClient client = new();

            try
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);

                await client.SendAsync(message);
            }
            catch (System.Exception e)
            {
                await client.DisconnectAsync(true);
                client.Dispose();
                Log.Error(e.Message);
                throw;
            }
        }

        private MimeMessage CreateEmail(EmailMessage emailMessage)
        {
            BodyBuilder builder = new() { HtmlBody = emailMessage.Body };

            var email = new MimeMessage
            {
                Subject = emailMessage.Subject,
                Body = builder.ToMessageBody()
            };

            email.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            email.To.Add(new MailboxAddress(emailMessage.ToAddress!.Split("@")[0], emailMessage.ToAddress));

            return email;
        }
    }
}