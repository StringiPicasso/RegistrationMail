using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Collections.Generic;

namespace RegistrationMail.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email)
        {
            //Генерируем и отправляем аподтверждение

            string verificationCode = GenerateVerificationCode();
            SendVerificationCode(email, verificationCode);

            //Передаем код потверждения в представление

            ViewBag.Email = email;
            ViewBag.VerificationCode = verificationCode;

            return View("VerifyCode");
        }

        [HttpPost]
        public IActionResult VerifyCode(string email, string code)
        {
            if (IsVerificationCodeValid(email, code))
            {
                //страница успеха
                return View("Success");
            }
            else
            {
                //сообщение ошибки
                ViewBag.ErrorMessage = "Неверный код подтверждения.";

                return View("VerifyCode");
            }
        }

        private string GenerateVerificationCode()
        {
            //Генерируем случайный код

            Random random = new Random();
            int code = random.Next(1000, 9999);

            return code.ToString();
        }

        private void SendVerificationCode(string email, string code)
        {
            //отправить код

            MailMessage message = new MailMessage();
            message.From = new MailAddress("your-email@example.com");
            message.To.Add(email);
            message.Subject = "Код подтверждения";
            message.Body = $"Ваш код подтверждения: {code}";

            SmtpClient smtp = new SmtpClient("smtp.example.com", 25);
            smtp.Credentials = new System.Net.NetworkCredential("your-username", "your-password");
            smtp.Send(message);
        }

        private bool IsVerificationCodeValid(string email, string code)
        {
            //Можно добавить проверку кода подтверждения в базе данных или другим способом
            //Просто здесь сравниваем код сгенерированный системой с введеным ползователем

            return code == GetVerificationCodeFromDatabase(email);
        }

        private string GetVerificationCodeFromDatabase(string email)
        {
            //Здесь должен быть код для получения кода подтверждения из базы данных
            //здесь возвращаем фиктивное значение
            return "1234";
        }
    }
}
