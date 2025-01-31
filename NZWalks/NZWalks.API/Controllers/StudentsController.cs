using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // https://localhost:5001/api/students
    [Route("api/[controller]")] // [controller] is automatically replaced with students by ASP
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:5001/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[]             {
                "John", "Jane", "Jack", "Jill"
            };

            return Ok(studentNames);
        }
    }
}
