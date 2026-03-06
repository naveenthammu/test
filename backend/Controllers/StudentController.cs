using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using simplebackend.Models;

namespace simplebackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _config;

        public StudentController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            var connString = _config.GetConnectionString("DefaultConnection");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                var query = "INSERT INTO students(name, roll_number) VALUES(@name,@roll)";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@roll", student.RollNumber);
                cmd.ExecuteNonQuery();
            }

            return Ok("Student Stored Successfully");
        }
    }
}