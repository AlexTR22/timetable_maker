using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        public RoomService _roomsService;

        public RoomController(RoomService roomService)
        {
            _roomsService = roomService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetAllRooms() => _roomsService.GetAllRooms();

        //[HttpGet("{id:int}")]
        //public ActionResult<Room> GetById(int id)
        //    => _roomsService.GetRoomById(id) is { } room ? Ok(room) : NotFound();

        [HttpPost]
        public IActionResult Create([FromBody] Room room)
        {
            bool status = _roomsService.AddRoomInDatabase(room);
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
        public IActionResult Update(int id, [FromBody] Room room)
        {

            bool status = _roomsService.ModifyRoomInDatabase(room);
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
            bool status = _roomsService.DeleteRoomInDatabase(id);
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
