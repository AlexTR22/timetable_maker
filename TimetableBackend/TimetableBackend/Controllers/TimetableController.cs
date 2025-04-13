using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimetableController : ControllerBase
    {
        private UniversityTimetableMaker _timetableMaker;
        private Helper _helper;
        public TimetableController(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTimetable()
        {
            _timetableMaker = new UniversityTimetableMaker(100, 2, "UNITBV",_helper);
            Chromosome chromosome = _timetableMaker.Run();
            return Ok(chromosome.Fitness);
        }
    }
}
