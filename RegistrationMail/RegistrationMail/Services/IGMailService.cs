using RegistrationMail.Models;

namespace RegistrationMail.Services
{
    public interface IGMailService
    {
        public void SendMailAsync(MailData mailData);
    }
}
