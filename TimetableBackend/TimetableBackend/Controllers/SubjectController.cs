using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        public SubjectService _subjectService;
        public SubjectController(SubjectService SubjectService)
        {
            _subjectService = SubjectService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllSubjects()
        {
            List<Subject> Subjects = _subjectService.GetAllSubjects();
            return Ok(Subjects);
        }
    }
}
