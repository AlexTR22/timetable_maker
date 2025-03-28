﻿using Microsoft.AspNetCore.Mvc;
using TimetableBackend.Model;
using TimetableBackend.Service;

namespace TimetableBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        public CourseService _courseService;
        public CourseController()
        {
            _courseService = new CourseService();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllCourses()
        {
            List<Course> courses = _courseService.GetAllCourses();
            return Ok(courses);
        }
    }
}
