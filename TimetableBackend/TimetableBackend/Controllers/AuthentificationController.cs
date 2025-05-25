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


        //[ProducesResponseType(200)]
        //[HttpPost("login")]
        //public IActionResult Login([FromBody] User user)
        //{
        //if (_userService.CredentialsVerification(user))
        //{
        //HttpContext.Session.SetString("user", user.Name);
        //return Ok(new { message = "Logged in successfully" });
        //}
        //return Unauthorized("Invalid credentials");
        //}

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
