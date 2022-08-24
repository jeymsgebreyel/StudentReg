using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using StudentRegCore;
using StudentRegProj.Data;
using StudentRegProj.Migrations;
using StudentRegProj.Models;

namespace StudentRegProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRegProjContext _context;

        #region CONSTRUCTOR
        public StudentsController(StudentRegProjContext context)
        {
            _context = context;
        }
        #endregion

        #region MAIN METHODS
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
          if (_context.Student == null)
          {
              return NotFound();
          }

            //GET ALL STUDENT LIST
            var allStudents = await _context.Student.ToListAsync();
            List<StudentDTO> studentData = new List<StudentDTO>();
            foreach(var student in allStudents)
            {
                studentData.Add(ConvertStudentToDTO(student));
            }

            return Ok(studentData);
        }

        

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            //LINQ LAMBDA
            var student = await _context.Student.Where(x => x.Id == id).FirstOrDefaultAsync(); //var student = await _context.Student.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }

            StudentDTO dto = ConvertStudentToDTO(student);
            return Ok(dto);
        }

        //NEW METHOD
        //GET: api/Students/Registered
        [HttpGet("Registered")]
        public async Task<ActionResult<IEnumerable<Student>>> GetRegistered()
        {
            if (_context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Where(s => s.IsRegistered == true)
                .OrderBy(s => s.LastName)
                .ToListAsync();

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Data is valid, continue registration
            Student newStudent = ConvertDTOToStudent(student);
            newStudent.DateRegistered = DateTime.Today;

            _context.Student.Add(newStudent);
            if (await _context.SaveChangesAsync() > 0) //SUCCESS SAVE
            {
                return Ok(ConvertStudentToDTO(newStudent));
            }
            return BadRequest();

         
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Student == null)
            {
                return NotFound();
            }
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion

        #region HELPER

        private StudentDTO ConvertStudentToDTO(Student student)
        {
            StudentDTO dto = new StudentDTO
            {
                LastName = student.LastName,
                Course = student.Course,
                FirstName = student.FirstName,
                IsRegistered = student.IsRegistered,
                YearLevel = student.YearLevel,
                Id = student.Id
            };
            return dto;
        }


        private Student ConvertDTOToStudent(StudentDTO student)
        {
            Student dto = new Student
            {
                LastName = student.LastName,
                Course = student.Course,
                FirstName = student.FirstName,
                IsRegistered = student.IsRegistered,
                YearLevel = student.YearLevel,
                Id = student.Id
            };
            return dto;
        }

        //DRY - Do not Repeat Your Code
        #endregion
    }
}
