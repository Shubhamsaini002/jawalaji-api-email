using emailapi.Business.Services;
using emailapi.Data;
using emailapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace emailapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersServices _userservice;

        public UsersController(UsersServices service)
        {
            _userservice = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(signupRequestVM user)
        {
            try
            {
                var result = await _userservice.Signup(user,"");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("login")]
        public async Task<IActionResult> login(string Email, string Password)
        {
            try
            {
                var result = await _userservice.Login(Email,Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email, string code, string password)
        {
            try
            {
                var result = await _userservice.ForgetPassword( email,  code, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("OtpVerification")]
        public async Task<IActionResult> OtpVerification(string Email,string code)
        {
            try
            {
                var result = await _userservice.OtpVerification( Email, code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ResendOTP")]
        public async Task<IActionResult> ResendOTP(SendOtpRequest data)
        {
            try
            {
                var result = await _userservice.SendOTP(data.Email, data.type);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Contactus")]
        public async Task<IActionResult> ContactUs(ContactUsVM data)
        {
            try
            {
                var result = await _userservice.Contactus(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getUserDetails")]
        public async Task<IActionResult> getUserDetails(string Email)
        {
            try
            {
                var result = await _userservice.getUserDetails(Email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails(UserDetailsVM data)
        {
            try
            {
                var result = await _userservice.UpdateUserDetails(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
