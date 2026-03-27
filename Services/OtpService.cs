using MimeKit;

namespace EmailApi.Services
{
    public class OtpService
    {
        private static Dictionary<string, (string Otp, DateTime Expiry)> _otpStore
            = new();

        public string GenerateOtp(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            _otpStore[email] = (otp, DateTime.Now.AddMinutes(5));

            return otp;
        }


    }
}