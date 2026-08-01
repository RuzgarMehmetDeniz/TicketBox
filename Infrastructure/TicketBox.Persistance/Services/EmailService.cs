using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.Services;
using TicketBox.Persistance.Services;

namespace TicketBox.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendTicketEmailAsync(TicketEmailModel model, CancellationToken cancellationToken = default)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(model.RecipientEmail));
            message.Subject = $"Biletiniz Hazır — {model.EventTitle}";

            var builder = new BodyBuilder
            {
                HtmlBody = TicketEmailTemplateBuilder.Build(model)
            };
            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            // Gmail/Outlook: 587 portu için StartTls kullanılır.
            await client.ConnectAsync(_settings.SmtpServer, _settings.Port, SecureSocketOptions.StartTls, cancellationToken);
            await client.AuthenticateAsync(_settings.SenderEmail, _settings.Password, cancellationToken);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}