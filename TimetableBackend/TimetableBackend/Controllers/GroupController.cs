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

        public GroupController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Group>> GetAll() => _groupService.GetAllGroups();

        [HttpGet("{collegeId:int}")]
        public ActionResult<IEnumerable<Group>> GetAllGoupsByCollege(int collegeId) => _groupService.GetAllGroupsByCollege(collegeId);

        [HttpPost]
        public IActionResult Create([FromBody] Group group)
        {
            bool status = _groupService.AddGroupInDatabase(group);
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
        public IActionResult Update(int id, [FromBody] Group group)
        {
            bool status = _groupService.ModifyGroupInDatabase(group);
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
            bool status = _groupService.DeleteGroupInDatabase(id);
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
