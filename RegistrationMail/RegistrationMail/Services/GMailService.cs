using MailKit.Security;
using MailKit.Net.Smtp;
using MailKit;
using Microsoft.Extensions.Options;
using MimeKit;
using RegistrationMail.Models;
using System;

namespace RegistrationMail.Services
{
    public class GMailService : IGMailService
    {
        private readonly ILogger<GMailService> _logger;
        private readonly GmailSettings _settings;

        public GMailService(ILogger<GMailService> logger, IOptions<GmailSettings> settings)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async void SendMailAsync(MailData mailData)
        {
            try
            {//создание сообщения
                using (var message = new MimeMessage())
                {
                    message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
                    message.To.Add(new MailboxAddress(mailData.EmailToName, mailData.EmailToId));

                    //Добавление контента в Mime Message
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    message.Subject = mailData.EmaiSubject;
                    bodyBuilder.HtmlBody = mailData.EmailBody;
                    bodyBuilder.TextBody = mailData.EmailBody;

                    message.Body = bodyBuilder.ToMessageBody();

                    //Подключение и отправка сообщения
                    using (var smtp = new SmtpClient())
                    {
                        await smtp.ConnectAsync(_settings.Server, _settings.Port, SecureSocketOptions.Auto);
                        await smtp.AuthenticateAsync(_settings.UserName, _settings.Password);
                        await smtp.SendAsync(message);
                        await smtp.DisconnectAsync(true);
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(0, exception, "MailKit.Send failed attempt {0}");
            }
        }
    }
}
