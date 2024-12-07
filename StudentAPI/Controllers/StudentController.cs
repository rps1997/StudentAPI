using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private ApplicationDbContext _db;
        private readonly ILogger<StudentController> _logger;

        public StudentController(ApplicationDbContext db, ILogger<StudentController> logger)
        {
            this._db = db;
            _logger = logger;
        }

        [HttpGet]
        public List<StudentEntity> GetAll()
        {
            return _db.StudentRegister.ToList();
        }

        [HttpGet("GetStudentById")]
        public ActionResult<StudentEntity> GetStudentById(int sid)
        {
            _logger.LogInformation("Fetching all students");
            var studentbyId = this._db.StudentRegister.FirstOrDefault(a => a.id == sid);
            return studentbyId;
        }

        [HttpPost]
        public ActionResult<StudentEntity> AddStudent([FromBody] StudentEntity studentEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.StudentRegister.Add(studentEntity);
            _db.SaveChanges();
            return Ok(studentEntity);
        }

        [HttpPost("UpdateStudentById")]
        public ActionResult<StudentEntity> UpdateStudentById([FromBody] StudentEntity studentEntity, int id)
        {
            var studentbyId = this._db.StudentRegister.FirstOrDefault(a => a.id == id);


            if (studentbyId == null)
            {
                return NotFound(studentbyId);
            }

            studentbyId.Name = studentEntity.Name;
            studentbyId.Age = studentEntity.Age;
            studentbyId.Class = studentEntity.Class;

            _db.SaveChanges();
            return Ok();
        }

        [HttpPut("DeleteStudent")]
        public ActionResult<StudentEntity> DeleteStudentById(int id)
        {
            var studentbyId = this._db.StudentRegister.FirstOrDefault(a => a.id == id);


            if (studentbyId == null)
            {
                return NotFound(studentbyId);
            }

            _db.StudentRegister.Remove(studentbyId);
            _db.SaveChanges();
            return Ok();
        }
    }
}
