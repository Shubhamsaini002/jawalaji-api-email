using emailapi.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace emailapi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public EmailService(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public async Task<SendOtpResponse> SendOtpEmailAsync(string toEmail, string subject, string otp, bool type)
        {
            try
            {
                string template = type ? "otp-template.html" : "restpassword.html";

                var templatePath = Path.Combine(_env.ContentRootPath, "Templates", template);

                if (!File.Exists(templatePath))
                    throw new Exception("Template not found: " + templatePath);

                var htmlBody = await File.ReadAllTextAsync(templatePath);

                htmlBody = htmlBody.Replace("{{OTP}}", otp);

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Your App", "vk87vinay@gmail.com"));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;

                email.Body = new BodyBuilder
                {
                    HtmlBody = htmlBody
                }.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.CheckCertificateRevocation = false;
                await smtp.ConnectAsync(
                    "smtp.gmail.com",
                    587,
                    MailKit.Security.SecureSocketOptions.StartTls
                );
               
                await smtp.AuthenticateAsync(
                    "vk87vinay@gmail.com",
                    "cofwcysappyuzmpn"
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                
                return new SendOtpResponse
                {
                    Success = true,
                    Message= "OTP sent successfully"
                };
            }
            catch (Exception ex)
            {
                return new SendOtpResponse
                {
                    Success = true,
                    Message = ex.Message
                };
            }
        }

        public async Task<SendOtpResponse> SendjawalajiOtpEmailAsync(string toEmail, string subject, string otp, bool type)
        {
            try
            {
                string template = type ? "jawlajiotp-template.html" : "jawalajirestpassword.html";

                var templatePath = Path.Combine(_env.ContentRootPath, "Templates", template);

                if (!File.Exists(templatePath))
                    throw new Exception("Template not found: " + templatePath);

                var htmlBody = await File.ReadAllTextAsync(templatePath);

                htmlBody = htmlBody.Replace("{{OTP}}", otp);

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Your App", "vk87vinay@gmail.com"));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;

                email.Body = new BodyBuilder
                {
                    HtmlBody = htmlBody
                }.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.CheckCertificateRevocation = false;
                await smtp.ConnectAsync(
                    "smtp.gmail.com",
                    587,
                    MailKit.Security.SecureSocketOptions.StartTls
                );
                await smtp.AuthenticateAsync(
                    "vk87vinay@gmail.com",
                    "cofwcysappyuzmpn"
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return new SendOtpResponse
                {
                    Success = true,
                    Message = "OTP sent successfully"
                };
            }
            catch (Exception ex)
            {
                return new SendOtpResponse
                {
                    Success = true,
                    Message = ex.Message
                };
            }
        }
    }
}