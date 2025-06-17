using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    [ApiController]
    [Route("[controller]")]
    public class AuthentificationController : ControllerBase
    {
        public UserService _userService;
        private readonly IConfiguration _cfg;

        public AuthentificationController(UserService userService, IConfiguration cfg)
        {
            _cfg = cfg;
            _userService = userService;
        }


        [ProducesResponseType(200)]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var _user = _userService.ValidateUser(loginRequest.Username, loginRequest.Password, loginRequest.Email);
            if (_user== null) return Unauthorized();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Id.ToString()),
                new Claim(ClaimTypes.Role, _user.Role), 
                new Claim("collegeId", _user.UniversityId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["JwtKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), universityId = _user.UniversityId });
        }

        //[ProducesResponseType(200)]
        //[HttpPost("register")]
        //public IActionResult Register([FromBody] User user)
        //{
        //if (!_userService.CredentialsVerification(user))
        //{
        //_userService.RegisterNewUser(user);
        //HttpContext.Session.SetString("user", user.Name);
        //return Ok(new { message = "Registered successfully" });
        //}
        //return Unauthorized("Account already existing!");
        //}

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll() => _userService.GetAllUsers();

        //[HttpGet("{id:int}")]
        //public ActionResult<User> GetById(int id)
        //    => _userService.GetUserById(id) is { } user ? Ok(user) : NotFound();

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            bool status = _userService.AddUserInDatabase(user);
            if (status)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (id != user.Id) return BadRequest();
            bool status = _userService.ModifyUserInDatabase(user);
            if (status)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            bool status = _userService.DeleteUserInDatabase(id);
            if (status)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }


    }

}
