using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController : Controller
    {
        public UniversityService _universityService;

        public UniversityController(UniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<University>> GetAll() => _universityService.GetAllUniversities();

        //[HttpGet("{id:int}")]
        //public ActionResult<University> GetById(int id)
        //    => _universityService.GetUniversityById(id) is { } uni ? Ok(uni) : NotFound();

        [HttpPost]
        public IActionResult Create([FromBody] University uni)
        {
            bool status = _universityService.AddUniversityInDatabase(uni);
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
        public IActionResult Update(int id, [FromBody] University uni)
        {
            if (id != uni.Id) return BadRequest();
            bool status = _universityService.ModifyUniversityInDatabase(uni);
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
            bool status = _universityService.DeleteUniversityInDatabase(id);
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
