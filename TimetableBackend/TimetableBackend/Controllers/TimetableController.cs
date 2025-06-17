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
        private TimeConstraintService _timeConstraintService;
        private Helper _helper;

        public TimetableController(Helper helper,TimeConstraintService timeConstraintService)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
            _chromosomeToTimetable = new ChromosomeToTimetable();
            _timeConstraintService = timeConstraintService;
        }
        [HttpGet("{collegeId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTimetable(int collegeId)
        {
            _timetableMaker = new UniversityTimetableMaker(100,4, collegeId, false,3,_helper,_timeConstraintService);

            Chromosome chromosome = _timetableMaker.Run();
            List<GroupTimetable> groupsTimetable = new List<GroupTimetable>();
            groupsTimetable = _chromosomeToTimetable.GetTimetableForFrontend(chromosome.Genes);
            Console.WriteLine(chromosome.Fitness);
            return Ok(groupsTimetable);
        }
    }
}
