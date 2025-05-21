using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthentificationController : ControllerBase
    {
        public UserService _userService;

        AuthentificationController(UserService userService)
        {
            _userService = userService;
        }


        [ProducesResponseType(200)]
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (_userService.CredentialsVerification(user))
            {
                HttpContext.Session.SetString("user", user.Name);
                return Ok(new { message = "Logged in successfully" });
            }
            return Unauthorized("Invalid credentials");
        }

        [ProducesResponseType(200)]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (!_userService.CredentialsVerification(user))
            {
                _userService.RegisterNewUser(user);
                HttpContext.Session.SetString("user", user.Name);
                return Ok(new { message = "Registered successfully" });
            }
            return Unauthorized("Account already existing!");
        }
    }

}
