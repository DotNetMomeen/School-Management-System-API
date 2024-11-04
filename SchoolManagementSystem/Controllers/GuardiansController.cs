using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Models.ViewModel;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardiansController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public GuardiansController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/Guardians
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guardian>>> GetGuardians()
        {
            return await _context.Guardians.ToListAsync();
        }

        // GET: api/Guardians/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Guardian>> GetGuardian(int id)
        {
            var guardian = await _context.Guardians.FindAsync(id);

            if (guardian == null)
            {
                return NotFound();
            }

            return guardian;
        }

        //// PUT: api/Guardians/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGuardian(int id, Guardian guardian)
        //{
        //    if (id != guardian.GuardianId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(guardian).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GuardianExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<StudentGuardian>> UpdateStudentGuardian([FromForm] GuardianVM vm)
        {
            var studentList = JsonConvert.DeserializeObject<Student[]>(vm.studentStringify);

            Guardian guardian = _context.Guardians.Find(vm.GuardianId);
            guardian.GuardianId = vm.GuardianId;
            guardian.GuardianName=vm.GuardianName;
            guardian.Phone=vm.Phone;
            guardian.NIDNumber=vm.NIDNumber;
            guardian.Email=vm.Email;

            //Delete existing spots
            var existingStudent = _context.StudentGuardian.Where(x => x.GuardianId == guardian.GuardianId).ToList();
            foreach (var item in existingStudent)
            {
                _context.StudentGuardian.Remove(item);
            }

            //Add newly added spots
            foreach (var item in studentList)
            {
                var studentGuardian = new StudentGuardian
                {
                    GuardianId = guardian.GuardianId,
                    StudentId = item.StudentId

                };
                _context.Add(studentGuardian);
            }

            _context.Entry(guardian).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(guardian);
        }

        //// POST: api/Guardians
        //[HttpPost]
        //public async Task<ActionResult<Guardian>> PostGuardian([FromForm] Guardian guardian)
        //{
        //    _context.Guardians.Add(guardian);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetGuardian", new { id = guardian.GuardianId }, guardian);
        //}


        [HttpPost]
        public async Task<ActionResult<StudentGuardian>> PostTeacherSubject([FromForm] GuardianVM vm)
        {
            var studentList = JsonConvert.DeserializeObject<Student[]>(vm.studentStringify);

            Guardian guardian = new Guardian
            {
                GuardianName = vm.GuardianName,
                Phone = vm.Phone,
                NIDNumber = vm.NIDNumber,
                Email = vm.Email
            };


            foreach (var item in studentList)
            {
                var studentGuardian = new StudentGuardian
                {
                    Guardian = guardian,
                    GuardianId = guardian.GuardianId,
                    StudentId = item.StudentId
                };
                _context.Add(studentGuardian);
            }
            await _context.SaveChangesAsync();
            return Ok(guardian);
        }

        private bool GuardianExists(int id)
        {
            return _context.Guardians.Any(e => e.GuardianId == id);
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult<StudentGuardian>> DeleteStudentGuardian(int id)
        {
            Guardian guardian = _context.Guardians.Find(id);

            var existingStudent = _context.StudentGuardian.Where(x => x.GuardianId == guardian.GuardianId).ToList();
            foreach (var item in existingStudent)
            {
                _context.StudentGuardian.Remove(item);
            }
            _context.Entry(guardian).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return Ok(guardian);
        }

    }
}
