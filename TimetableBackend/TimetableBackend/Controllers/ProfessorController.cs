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
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllEvents()
        {
            List<Professor> professors = _professorService.GetAllProfessors();
            return Ok(professors);
        }


    }
}