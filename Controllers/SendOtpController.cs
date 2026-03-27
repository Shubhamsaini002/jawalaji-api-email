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

            var otp = _otpService.GenerateOtp(request.Email);

            var subject = "Your Email Verification OTP ";

            var result = await _emailService.SendOtpEmailAsync(
                request.Email,
                subject,
                otp,
                request.type
            );

            if (result)
            {
                return Ok(new SendOtpResponse
                {   Success= result,
                    Message = "OTP sent successfully"
                });
            }

            return BadRequest(  new SendOtpResponse
            {
                Success = result,
                Message = "Error please check your mail..."
            });
        }

        [HttpPost("jwalajiotp")]
        public async Task<IActionResult> SendForgetPassword([FromBody] SendOtpRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest("Email is required");

            var otp = _otpService.GenerateOtp(request.Email);

            var subject = "Your Forget Password Verification ";

            var result = await _emailService.SendjawalajiOtpEmailAsync(
                request.Email,
                subject,
                otp,
                request.type
            );

            if (result)
            {
                return Ok(new SendOtpResponse
                {
                    Success = result,
                    Message = "OTP sent successfully"
                });
            }

            return BadRequest(new SendOtpResponse
            {
                Success = result,
                Message = "Error please check your mail..."
            });
        }
    }
}