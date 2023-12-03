namespace RegistrationMail.Services
{
    public interface IVerifyCode
    {
        public string GenerateVerificationCode();
       public bool IsVerificationCodeValid(string email, string code);
        public string GetVerificationCodeFromDatabase(string email);
    }
}
