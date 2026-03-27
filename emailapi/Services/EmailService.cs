using MailKit.Net.Smtp;
using MimeKit;

namespace emailapi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<bool> SendOtpEmailAsync(string toEmail, string subject, string otp,bool type)
        {
            try
            {  string template =type? "otp-template.html" : "restpassword.html";
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", template);

                var htmlBody = await File.ReadAllTextAsync(templatePath);

                htmlBody = htmlBody.Replace("{{OTP}}", otp);

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Your App", _config["EmailSettings:Email"]));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;

                email.Body = new BodyBuilder
                {
                    HtmlBody = htmlBody
                }.ToMessageBody();

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(
                    _config["EmailSettings:Email"],
                    _config["EmailSettings:Password"]
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
              
                return false;
            }
        }

        public async Task<bool> SendjawalajiOtpEmailAsync(string toEmail, string subject, string otp, bool type)
        {
            try
            {
                string template = type ? "jawlajiotp-template.html" : "jawalajirestpassword.html";
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", template);

                var htmlBody = await File.ReadAllTextAsync(templatePath);

                htmlBody = htmlBody.Replace("{{OTP}}", otp);

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Your App", _config["EmailSettings:Email"]));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;

                email.Body = new BodyBuilder
                {
                    HtmlBody = htmlBody
                }.ToMessageBody();

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(
                    _config["EmailSettings:Email"],
                    _config["EmailSettings:Password"]
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
