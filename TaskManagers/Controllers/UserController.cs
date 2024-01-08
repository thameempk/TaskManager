using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagers.Models;

namespace TaskManagers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public  class UserController : ControllerBase
    {
        [HttpGet]
        public  IActionResult Login (User user)
        {
            return Ok(user);
        }
    }
}
