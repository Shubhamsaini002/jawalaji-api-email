using emailapi.Business.Services;
using emailapi.Models;
using Microsoft.AspNetCore.Mvc;


namespace studyapp.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService service)
        {
            _adminService = service;
        }
        [HttpPost("database")]
        public async Task<IActionResult> Database(adminVM data)
        {
            try
            { 
                if( data.Password != "Aa@12345")
                {
                    return BadRequest("Dont have Access...");
                }
                var result = await _adminService.database(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
