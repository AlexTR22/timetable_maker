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
        public ActionResult<IEnumerable<Subject>> GetAll() => _subjectService.GetAllSubjects();

        [HttpGet("{collegeId:int}")]
        public ActionResult<IEnumerable<Subject>> GetAllSubjectsByCollege(int collegeId) => _subjectService.GetAllSubjectsByCollege(collegeId);
        //[HttpGet("{id:int}")]
        //public ActionResult<Subject> GetById(int id)
        //    => _subjectService.GetSubjectById(id) is { } subj ? Ok(subj) : NotFound();

        [HttpPost]
        public IActionResult Create([FromBody] Subject subject)
        {
            //Console.WriteLine(subject.IdCollege);
            bool status = _subjectService.AddSubjectInDatabase(subject);
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
        public IActionResult Update(int id, [FromBody] Subject subject)
        {

            bool status = _subjectService.ModifySubjectInDatabase(subject);
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
            bool status = _subjectService.DeleteSubjectInDatabase(id);
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
