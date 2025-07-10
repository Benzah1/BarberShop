using Application.DTOs;
using Application.Interfaces;
using Domain.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmail(EmailDTO dto)
        {
            var smtpClient = new SmtpClient(_settings.SmtpServer)
            {
                Port = _settings.SmtpPort,
                Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail, "BarberShop"), // puedes agregar un nombre de remitente fijo
                Subject = dto.Subject,
                Body = dto.Body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(dto.To);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
