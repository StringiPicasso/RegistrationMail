using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Collections.Generic;
using RegistrationMail.Models;
using RegistrationMail.Services;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace RegistrationMail.Controllers
{
    public class UserController : Controller
    {
        private readonly IGMailService _mailService;
        private readonly IVerifyCode _verifyCode;
        public UserController(IGMailService mailService,IVerifyCode verifyCode)
        {
            _mailService = mailService;
            _verifyCode = verifyCode;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email)
        {
            //Генерируем и отправляем аподтверждение
            string verificationCode = _verifyCode.GenerateVerificationCode();

            MailData mailData = new MailData()
            {
                EmaiSubject = "Код подтверждения",
                EmailToId = email,
                EmailToName = "Anonim",
                EmailBody = $"Ваш код подтверждения: {verificationCode}",
            };

            _mailService.SendMailAsync(mailData);

            //Передаем код потверждения в представление

            ViewBag.Email = email;
            ViewBag.VerificationCode = verificationCode;

            return View("VerifyCode");
        }

        [HttpPost]
        public IActionResult VerifyCode(string email, string code)
        {
            if (_verifyCode.IsVerificationCodeValid(email, code))
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
    }
}
