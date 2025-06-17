using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeConstraintsController : ControllerBase
    {
        public TimeConstraintService _TCService;

        public TimeConstraintsController(TimeConstraintService TCService)
        {
            _TCService = TCService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TimeConstraint>> GetAll() => _TCService.GetAllTimes();

        [HttpGet("{collegeId:int}")]
        public ActionResult<IEnumerable<TimeConstraint>> GetAllTimeConstraintsByCollege(int collegeId) => _TCService.GetAllTimeConstraintsByCollege(collegeId);
        //[HttpGet("{id:int}")]
        //public ActionResult<TimeConstraint> GetById(int id)
        //    => _TCService.GetTimeConstraintById(id) is { } tc ? Ok(tc) : NotFound();

        [HttpPost]  
        public IActionResult Create([FromBody] TimeConstraint tc)
        {
            bool status = _TCService.AddTimeConstraintInDatabase(tc);
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
        public IActionResult Update(int id, [FromBody] TimeConstraint tc)
        {
            bool status = _TCService.ModifyTimeConstraintInDatabase(tc);
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
            bool status = _TCService.DeleteTimeConstraintInDatabase(id);
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
