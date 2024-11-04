using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using System;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusClassController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CampusClassController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/CampusClass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampusClass>>> GetCampusClasses()
        {
            return await _context.CampusClasses.ToListAsync();
        }

        // GET: api/CampusClass/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampusClass>> GetCampusClass(int id)
        {
            var campusClass = await _context.CampusClasses.FindAsync(id);

            if (campusClass == null)
            {
                return NotFound();
            }

            return campusClass;
        }

        // POST: api/CampusClass
        [HttpPost]
        public async Task<ActionResult<CampusClass>> PostCampusClass(CampusClass campusClass)
        {
            _context.CampusClasses.Add(campusClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampusClass", new { id = campusClass.CampusClassId }, campusClass);
        }

        // PUT: api/CampusClass/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampusClass(int id, CampusClass campusClass)
        {
            if (id != campusClass.CampusClassId)
            {
                return BadRequest();
            }

            _context.Entry(campusClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampusClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Data Updated successfully.");
        }

        // DELETE: api/CampusClass/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampusClass(int id)
        {
            var campusClass = await _context.CampusClasses.FindAsync(id);
            if (campusClass == null)
            {
                return NotFound();
            }

            _context.CampusClasses.Remove(campusClass);
            await _context.SaveChangesAsync();

            return Ok("Data Deleted successfully.");
        }

        private bool CampusClassExists(int id)
        {
            return _context.CampusClasses.Any(e => e.CampusClassId == id);
        }
    }
}
