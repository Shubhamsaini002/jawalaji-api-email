using emailapi.Business.Services;
using emailapi.Data;
using emailapi.Models;
using emailapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace emailapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : Controller
    {
        private readonly UserTaskAndServices _userservice;
        public UserServiceController(UserTaskAndServices userservice) {
        _userservice = userservice;
        }
        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService(createServiceVM data)
        {
            try
            {
                var result = await _userservice.CreateService(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetService")]
        public async Task<IActionResult> GetService(int userid)
        {
            try
            {
                var result = await _userservice.GetService(userid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask(CreateTaskVM data)
        {
            try
            {
                var result = await _userservice.CreateTask(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTask")]
        public async Task<IActionResult> GetTask(int serviceid)
        {
            try
            {
                var result = await _userservice.GetTask(serviceid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateTask")]
        public async Task<IActionResult> UpdateTask(SubTasks data)
        {
            try
            {
                var result = await _userservice.UpdateTask(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateService")]
        public async Task<IActionResult> UpdateService(UserServices data)
        {
            try
            {
                var result = await _userservice.UpdateService(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
