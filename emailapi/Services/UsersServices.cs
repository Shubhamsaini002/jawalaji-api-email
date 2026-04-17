using emailapi.Data;
using emailapi.Models;
using emailapi.Services;
using EmailApi.Data;
using Microsoft.EntityFrameworkCore;


namespace emailapi.Business.Services
{
    public class UsersServices 
    {
        private readonly AppDbContext _context;
        private readonly EmailService _sendMail;

        public UsersServices(AppDbContext context, EmailService sendMail)
        {
            _context = context;
            _sendMail = sendMail;
        }

        public async Task<ResponseVM> Signup(signupRequestVM user,string country)
        {
            var random = new Random();
            int otp = random.Next(100000, 1000000);
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (existingUser != null )
            { if(existingUser.Verified == true)
                {
                    return new ResponseVM()
                    {
                        status = 0,
                        Message = "already has an account.",
                    };
                }
               
                existingUser.Name = user.Name;
                existingUser.VerificationCode = otp.ToString();
                existingUser.Password = user.Password;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
           
            if (existingUser == null)
            {
                Users newuser = new Users()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    VerificationCode = otp.ToString(),
                    Country= country,
                    CreatedAt = DateTime.UtcNow,
                    Verified = false,
                  
                };
       
                    _context.Users.Add(newuser);
                await _context.SaveChangesAsync();
            }
            var res = await _sendMail.SendOtpEmailAsync(user.Email, "Verification Code", otp.ToString(),true);
            return new ResponseVM()
            {
                status = 1,
                Message = "OTP has sent to your Email Account.",

            };

        }

        public async Task<ResponseVM> Login(string Email, string Password)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email && x.Password == Password && x.Verified==true);
            if (existingUser == null)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = "Please Check Your Email Or Password!",
                };
            }

            return new ResponseVM()
            {
                status = 1,
                Message = "Verified successfully",
                data = new UserDetailsVM()
                {
                    Id = existingUser.Id,
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                    PhoneNumber = existingUser.PhoneNumber,
                    Country = existingUser.Country,
                }
            };
        }

        public async Task<ResponseVM> OtpVerification(string Mail, string code)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == Mail && x.VerificationCode==code);
            if (existingUser == null) {
                return new ResponseVM()
                {
                    status = 0,
                    Message = "Invalid OTP!",
                };
            }
            
            existingUser.Verified =true;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return new ResponseVM()
            {
                status = 1,
                Message = "Verified successfully",
                data = new UserDetailsVM()
                {
                    Id = existingUser.Id,
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                    PhoneNumber = existingUser.PhoneNumber,
                    Country = existingUser.Country,
                }
            };
        }

        public async Task<ResponseVM> ForgetPassword(string Mail, string code, string password)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == Mail && x.VerificationCode == code);
            if (existingUser == null)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = "Invalid OTP!",
                };
            }

            existingUser.Password = password;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return new ResponseVM()
            {
                status = 1,
                Message = "New Password Genrated!",
                data = new UserDetailsVM()
                {
                    Id = existingUser.Id,
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                    PhoneNumber = existingUser.PhoneNumber,
                    Country = existingUser.Country,
                }
            };
        }


        public async Task<ResponseVM> SendOTP(string Mail,bool type)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == Mail);
            if (existingUser == null)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = "Account does not exist.",
                };
            }
            var random = new Random();
            int otp = random.Next(100000, 1000000);
            existingUser.VerificationCode = otp.ToString();
            await _sendMail.SendOtpEmailAsync(Mail,"Verification OTP", otp.ToString(), type);
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return new ResponseVM()
            {
                status = 1,
                Message = "OTP has sent to your Email Account.",
            };

        }

        public async Task<ResponseVM> Contactus(ContactUsVM data)
        {
            ContactUs req = new ContactUs()
            {   Name=data.Name,
                CreatedAt= DateTime.UtcNow,
                Email=data.Email,
                Subject=data.Subject,
                Message=data.Message,
                Status=false,

            };
            _context.ContactUs.Add(req);
            await _context.SaveChangesAsync();
            return new ResponseVM()
            {
                status = 1,
                Message = "We Will Connect You Soon..",
            };

        }
        public async Task<ResponseVM> getUserDetails(string Email)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (existingUser == null)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = "No Details!",
                };
            }
            return new ResponseVM()
            {
                status = 1,
                Message = "Details Found",
                data = new UserDetailsVM()
                {
                    Id = existingUser.Id,
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                    PhoneNumber = existingUser.PhoneNumber,
                    Country = existingUser.Country,
                }
            };
        }

        public async Task<ResponseVM> UpdateUserDetails(UserDetailsVM data)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == data.Id);
            if (existingUser == null)
            {
                return new ResponseVM()
                {
                    status = 0
                };
                
            }
            existingUser.PhoneNumber = data.PhoneNumber;
            existingUser.Country = data.Country;
            existingUser.Name = data.Name;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return new ResponseVM()
            {
                status = 1,
                Message = "Details Found",
                data = new UserDetailsVM()
                {
                    Id = existingUser.Id,
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                    PhoneNumber = existingUser.PhoneNumber,
                    Country = existingUser.Country,
                }
            };
        }
    }
}
