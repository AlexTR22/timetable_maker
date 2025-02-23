using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class ProfessorController: ControllerBase
    {
        public ProfessorServicecs _professorService;

        public ProfessorController()
        {
            _professorService = new ProfessorServicecs();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllEvents()
        {
            List<Professor> events =  _professorService.GetAllProfessors();
            return Ok(events);
        }


    }
}