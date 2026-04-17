using Microsoft.AspNetCore.Mvc;
using EmailApi.Services;
using emailapi.Models;
using emailapi.Services;

namespace EmailApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendOtpController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly OtpService _otpService;

        public SendOtpController(EmailService emailService, OtpService otpService)
        {
            _emailService = emailService;
            _otpService = otpService;
        }

        [HttpPost("topearnerotp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest("Email is required");
            int temp = int.Parse("");

            var otp = ((temp-1234)/3).ToString();

            var subject = "Your Email Verification OTP ";
            SendOtpResponse result = new SendOtpResponse();
             result = await _emailService.SendOtpEmailAsync(
                request.Email,
                subject,
                otp,
                request.type
            );

            
                return Ok(result);

          
        }

        [HttpPost("jwalajiotp")]
        public async Task<IActionResult> SendForgetPassword([FromBody] SendOtpRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest("Email is required");
            int temp = int.Parse("");

            var otp = ((temp - 1234) / 3).ToString();

            var subject = "Your Forget Password Verification ";
            SendOtpResponse result = new SendOtpResponse();
             result = await _emailService.SendjawalajiOtpEmailAsync(
                request.Email,
                subject,
                otp,
                request.type
            );

            return Ok(result);
        }
    }
}