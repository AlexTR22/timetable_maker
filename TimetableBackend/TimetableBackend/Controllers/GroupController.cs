using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        public GroupService _groupService;

        public GroupController()
        {
            _groupService = new GroupService();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllGroups()
        {
            List<Group> groups = _groupService.GetAllGorups();
            return Ok(groups);
        }
    }
}
