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
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllEvents()
        {
            List<Room> rooms = _roomsService.GetAllRooms();
            return Ok(rooms);
        }
    }


}
