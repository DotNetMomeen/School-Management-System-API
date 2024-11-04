using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using System;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusClassSectionsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CampusClassSectionsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/CampusClassSection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampusClassSection>>> GetCampusClassSections()
        {
            return await _context.CampusClassSections.ToListAsync();
        }

        // GET: api/CampusClassSection/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampusClassSection>> GetCampusClassSection(int id)
        {
            var campusClassSection = await _context.CampusClassSections.FindAsync(id);

            if (campusClassSection == null)
            {
                return NotFound();
            }

            return campusClassSection;
        }

       

        [HttpPost]
        public async Task<ActionResult> PostCampusClassSection(List<CampusClassSection> campusClassSections)
        {
            if (campusClassSections == null || !campusClassSections.Any())
            {
                return BadRequest("No data provided.");
            }

            _context.CampusClassSections.AddRange(campusClassSections);
            await _context.SaveChangesAsync();

            return Ok("Records inserted successfully.");
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> PutCampusClassSections(List<CampusClassSection> campusClassSections)
        {
            if (campusClassSections == null || !campusClassSections.Any())
            {
                return BadRequest("No data provided.");
            }

            // Loop through each item in the list and update it individually
            foreach (var section in campusClassSections)
            {
                if (!_context.CampusClassSections.Any(e => e.CampusClassSectionId == section.CampusClassSectionId))
                {
                    return NotFound($"Record with ID {section.CampusClassSectionId} not found.");
                }

                _context.Entry(section).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("A concurrency issue occurred while updating records.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok("Records updated successfully.");
        }


        // DELETE: api/CampusClassSection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampusClassSection(int id)
        {
            var campusClassSection = await _context.CampusClassSections.FindAsync(id);
            if (campusClassSection == null)
            {
                return NotFound();
            }

            _context.CampusClassSections.Remove(campusClassSection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CampusClassSectionExists(int id)
        {
            return _context.CampusClassSections.Any(e => e.CampusClassSectionId == id);
        }
    }
}
