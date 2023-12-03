using RegistrationMail.Models;

namespace RegistrationMail.Services
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);

        Task<bool> SendMailAsync(MailData mailData);
    }
}
