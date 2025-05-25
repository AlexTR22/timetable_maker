using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;
using static TimetableBackend.Service.ChromosomeToTimetable;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimetableController : ControllerBase
    {
        private readonly ChromosomeToTimetable _chromosomeToTimetable;

      
        private UniversityTimetableMaker _timetableMaker;
        private Helper _helper;
        public TimetableController(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
            _chromosomeToTimetable = new ChromosomeToTimetable();
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTimetable()
        {
            _timetableMaker = new UniversityTimetableMaker(100, 2, "FMI",false,3,_helper);
            Chromosome chromosome = _timetableMaker.Run();
            List<GroupTimetable> groupsTimetable = new List<GroupTimetable>();
            groupsTimetable = _chromosomeToTimetable.GetTimetableForFrontend(chromosome.Genes);
            Console.WriteLine(chromosome.Fitness);
            return Ok(groupsTimetable);
        }
    }
}
