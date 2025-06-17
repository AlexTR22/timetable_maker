using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public ActionResult<IEnumerable<Professor>> GetAll()
        {
            return Ok(_professorService.GetAllProfessors());
        }

        [HttpGet("{collegeId:int}")]
        public ActionResult<IEnumerable<Professor>> GetAllProfessorsByCollege(int collegeId) => _professorService.GetAllProfessorsByCollege(collegeId);

        [HttpGet("getProf/{id:int}")]
        public ActionResult<Professor> GetById(int id)
        {
            Professor professor = _professorService.GetProfessorById(id);
            Console.WriteLine(professor);
            return Ok( new { professor = professor });
        }

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

        [HttpPut]
        public IActionResult Update([FromBody] Professor professor)
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

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetProfessorIdByName(string name)
        {
            Console.WriteLine(name);
            int id = _professorService.GetProfessorIdByName(name);
            Console.WriteLine(id);
            if (id == null)
                return NotFound();

            return Ok(new { professorId = id });
        }

    }
}