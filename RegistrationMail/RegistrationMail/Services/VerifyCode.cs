namespace RegistrationMail.Services
{
    public class VerifyCode : IVerifyCode
    {
        public string GenerateVerificationCode()
        {
            //Генерируем случайный код

            Random random = new Random();
            int code = random.Next(1000, 9999);

            return code.ToString();
        }


        public bool IsVerificationCodeValid(string email, string code)
        {
            //Можно добавить проверку кода подтверждения в базе данных или другим способом
            //Просто здесь сравниваем код сгенерированный системой с введеным ползователем

            return code == GetVerificationCodeFromDatabase(email);
        }
        public string GetVerificationCodeFromDatabase(string email)
        {
            //Здесь должен быть код для получения кода подтверждения из базы данных
            //здесь возвращаем фиктивное значение
            return "1234";
        }
    }
}
