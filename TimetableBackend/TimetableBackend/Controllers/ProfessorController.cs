using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class ProfessorController: ControllerBase
    {
        public ProfessorService _professorService;

        public ProfessorController(ProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Professor>> GetAll() => _professorService.GetAllProfessors();

        //[HttpGet("{id:int}")]
        //public ActionResult<Professor> GetById(int id)
        //    => _professorService.GetProfessorById(id) is { } prof ? Ok(prof) : NotFound();

        [HttpPost]
        public IActionResult Create([FromBody] Professor professor)
        {
            bool status = _professorService.AddProfessorInDatabase(professor);
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
        public IActionResult Update(int id, [FromBody] Professor professor)
        {
            bool status = _professorService.ModifyProfessorInDatabase(professor);
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
            bool status = _professorService.DeleteProfessorInDatabase(id);
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