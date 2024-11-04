using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Models.ViewModel;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculamsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CurriculamsController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCurriculum")]
        public async Task<ActionResult<IEnumerable<Curriculum>>> GetCurriculum()
        {
            return await _context.Curriculum.Include(c => c.Subjects).Include(c => c.Campuses).ToListAsync();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurruculumVM>>> GetallCurriculum()
        {
            List<CurruculumVM> CurrCampussList = new List<CurruculumVM>();
            List<CurruculumVM> subjectList = new List<CurruculumVM>();



            var allcurricu = _context.Curriculum.Include(c => c.Subjects).Include(c => c.Campuses).ToList();

            List<CurruculumVM> crvm = new List<CurruculumVM>();
            allcurricu.ForEach(a => crvm.Add(new CurruculumVM(a.CurriculumId, a.CurriculumName, a.Description, a.Campuses, a.Subjects)));

            return crvm;
       
        }
 
        // PUT: api/Curriculams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurriculum(int id, Curriculum curriculum)
        {
            if (id != curriculum.CurriculumId)
            {
                return BadRequest();
            }

            _context.Entry(curriculum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurriculumExists(id))
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

        // POST: api/Curriculams
        [HttpPost]
        public async Task<ActionResult<Curriculum>> PostCurriculum(Curriculum curriculum)
        {
            _context.Curriculum.Add(curriculum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurriculum", new { id = curriculum.CurriculumId }, curriculum);
        }

        // DELETE: api/Curriculams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurriculum(int id)
        {
            var curriculum = await _context.Curriculum.FindAsync(id);
            if (curriculum == null)
            {
                return NotFound();
            }

            _context.Curriculum.Remove(curriculum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurriculumExists(int id)
        {
            return _context.Curriculum.Any(e => e.CurriculumId == id);
        }
    }
}
