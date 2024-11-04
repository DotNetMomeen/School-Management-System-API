using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using System;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCampusClassSectionsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentCampusClassSectionsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentCampusClassSection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCampusClassSection>>> GetStudentCampusClassSections()
        {
            return await _context.StudentCampusClassSections.ToListAsync();
        }

        // GET: api/StudentCampusClassSection/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentCampusClassSection>> GetStudentCampusClassSection(int id)
        {
            var studentCampusClassSection = await _context.StudentCampusClassSections.FindAsync(id);

            if (studentCampusClassSection == null)
            {
                return NotFound();
            }

            return studentCampusClassSection;
        }

        // POST: api/StudentCampusClassSection
        //[HttpPost]
        //public async Task<ActionResult<StudentCampusClassSection>> PostStudentCampusClassSection(StudentCampusClassSection studentCampusClassSection)
        //{
        //    _context.StudentCampusClassSections.Add(studentCampusClassSection);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStudentCampusClassSection", new { id = studentCampusClassSection.StudentCampusClassSectionId }, studentCampusClassSection);
        //}

        [HttpPost]
        public async Task<ActionResult> PostStudentCampusClassSections(List<StudentCampusClassSection> studentCampusClassSections)
        {
            if (studentCampusClassSections == null || !studentCampusClassSections.Any())
            {
                return BadRequest("No data provided.");
            }

            // Add the list of StudentCampusClassSection to the context
            _context.StudentCampusClassSections.AddRange(studentCampusClassSections);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok("Records inserted successfully.");
        }


        // PUT: api/StudentCampusClassSection/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentCampusClassSections(List<StudentCampusClassSection> studentCampusClassSections)
        {
            if (studentCampusClassSections == null || !studentCampusClassSections.Any())
            {
                return BadRequest("No data provided.");
            }

            // Iterate through each studentCampusClassSection object
            foreach (var item in studentCampusClassSections)
            {
                // Check if the record exists in the database
                var existingItem = await _context.StudentCampusClassSections.FindAsync(item.StudentCampusClassSectionId);
                if (existingItem == null)
                {
                    return NotFound($"Record with ID {item.StudentCampusClassSectionId} not found.");
                }

                // Update the properties of the existing item with the new values
                existingItem.StudentId = item.StudentId;
                existingItem.CampusClassSectionId = item.CampusClassSectionId;

                // Optionally, you can also update other related properties if needed
            }

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, $"Concurrency error: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok("Data Updated successfully.");
        }


        // DELETE: api/StudentCampusClassSection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentCampusClassSection(int id)
        {
            var studentCampusClassSection = await _context.StudentCampusClassSections.FindAsync(id);
            if (studentCampusClassSection == null)
            {
                return NotFound();
            }

            _context.StudentCampusClassSections.Remove(studentCampusClassSection);
            await _context.SaveChangesAsync();

            return Ok("Data Deleted successfully.");
        }

        private bool StudentCampusClassSectionExists(int id)
        {
            return _context.StudentCampusClassSections.Any(e => e.StudentCampusClassSectionId == id);
        }
    }
}
