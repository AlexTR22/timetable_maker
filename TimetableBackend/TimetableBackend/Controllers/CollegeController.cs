using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollegeController : Controller
    {
        public CollegeService _collegeService;

        public CollegeController(CollegeService collegeService)
        {
            _collegeService = collegeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<College>> GetAll() => _collegeService.GetAllColleges();

        //[HttpGet("{id:int}")]
        //public ActionResult<College> GetById(int id)
        //    => _collegeService.GetCollegeById(id) is { } col ? Ok(col) : NotFound();

        [HttpPost]
        public IActionResult Create([FromBody] College college)
        {
            bool status = _collegeService.AddCollegeInDatabase(college);
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
        public IActionResult Update(int id, [FromBody] College college)
        {
            bool status = _collegeService.ModifyCollegeInDatabase(college);
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
            bool status = _collegeService.DeleteCollegeInDatabase(id);
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
