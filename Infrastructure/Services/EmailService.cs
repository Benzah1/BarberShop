using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService :IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(EmailDTO dto)
        {
            var settings = _config.GetSection("EmailSettings");

            var smtpClient = new SmtpClient(settings["SmtpServer"])
            {
                Port = int.Parse(settings["SmtpPort"]!),
                Credentials = new NetworkCredential(settings["SenderEmail"], settings["SenderPassword"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(settings["SenderEmail"]!, settings["SenderName"]),
                Subject = dto.Subject,
                Body = dto.Body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(dto.To);
            await smtpClient.SendMailAsync(mailMessage);
        }

    }
}
