using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagers.Models;

namespace TaskManagers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _users;
        private readonly IConfiguration _configuration;
        public UserController(IUsers users, IConfiguration configuration)
        {
            _users = users;
            _configuration = configuration;
        }
        [HttpGet(Name = "getUsers")]
        public ActionResult GetUsers()
        {
            return Ok(_users.GetUsers());
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user.User_Id > 0)
            {
                return BadRequest();
            }
            var isExist = _users.GetUsers().FirstOrDefault(s => s.Name == user.Name);
            if (isExist != null)
            {
                return BadRequest("user already exist");
            }
            user.User_Id = _users.GetUsers().OrderByDescending(s => s.User_Id).FirstOrDefault().User_Id + 1;
            _users.Register(user);
            return CreatedAtRoute("getUsers", new { id = user.User_Id }, user);
        }


        [HttpPost("login")]

        public IActionResult Login([FromBody] Login user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                var ExistingUser = _users.Login(user);
                if (ExistingUser == null)
                {
                    return Unauthorized("invalid username or password");
                }

                string token = GenerateJwtToken(ExistingUser);
                return Ok(new { Token = token });

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}");
            }

        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.User_Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var token = new JwtSecurityToken(
                //issuer: _configuration["Jwt:Issuer"],
                //audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(1)

            ); ;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
