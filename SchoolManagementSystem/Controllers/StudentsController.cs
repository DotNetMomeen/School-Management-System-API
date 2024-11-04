using System;
using System.Collections.Generic;
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
    public class StudentsController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly IWebHostEnvironment _env;
        public StudentsController(SchoolDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCampusClassSection>>> GetStudents()
        {
            return await _context.StudentCampusClassSections.Include(x => x.Student).Include(x => x.Student!.Gender).Include(sccs => sccs.CampusClassSection).ThenInclude(sccs => sccs!.CampusClass).ThenInclude(sccs => sccs!.Campus).Include(scss => scss.CampusClassSection!.Section).Include(scss => scss.CampusClassSection!.CampusClass!.Class).ToListAsync();

        }



        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStudent([FromForm]int id, StudentVM vm)
        //{
        //    //if (id != vm.StudentId)
        //    //{
        //    //    return BadRequest();
        //    //}

        //    Student student = _context.Students.Find(vm.StudentId);
        //    student.StudentId=vm.StudentId;
        //    student.StudentFName = vm.StudentFName;
        //    student.StudentLName = vm.StudentLName;
        //    student.FatherName = vm.FatherName;
        //    student.MotherName = vm.MotherName;
        //    student.BirthCertificateNumber = vm.BirthCertificateNumber;
        //    student.DateOfBirth = vm.DateOfBirth;
        //    student.Address = vm.Address;
        //    student.GenderId = vm.GenderId;
        //    student.Image = vm.Image;

        //    if (vm.ImagePath != null)
        //    {
        //        var webroot = _env.WebRootPath;
        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImagePath.FileName);
        //        var filePath = Path.Combine(webroot, "Images", fileName);

        //        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        //        await vm.ImagePath.CopyToAsync(fileStream);
        //        await fileStream.FlushAsync();
        //        fileStream.Close();
        //        student.Image = fileName;
        //    }


        //    var campusClass = new CampusClass
        //    {
        //        CampusId = vm.CampusId,
        //        ClassId = vm.ClassId,
        //    };
        //    _context.Add(campusClass);

        //    var campusClassSection = new CampusClassSection
        //    {
        //        CampusClass = campusClass,
        //        CampusClassId = campusClass.CampusClassId,
        //        SectionId = vm.SectionId,
        //    };
        //    _context.Add(campusClassSection);

        //    var studentCampusClassSection = new StudentCampusClassSection
        //    {
        //        CampusClassSection = campusClassSection,
        //        StudentCampusClassSectionId = campusClassSection.CampusClassSectionId,
        //        Student = student,
        //        StudentId = student.StudentId
        //    };
        //    _context.Add(studentCampusClassSection);

        //    await _context.SaveChangesAsync();
        //    return Ok(student);
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> PutStudent(int id, [FromForm] StudentVM vm)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.StudentFName = vm.StudentFName;
            student.StudentLName = vm.StudentLName;
            student.FatherName = vm.FatherName;
            student.MotherName = vm.MotherName;
            student.DateOfBirth = vm.DateOfBirth;
            student.BirthCertificateNumber = vm.BirthCertificateNumber;
            student.Address = vm.Address;
            student.GenderId = vm.GenderId;

            if (vm.ImagePath != null)
            {
                var webroot = _env.WebRootPath;
                var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(vm.ImagePath.FileName);
                var filePath = Path.Combine(webroot, "Images", fileName);

                // Delete the existing image if necessary
                if (!string.IsNullOrEmpty(student.Image))
                {
                    var oldImagePath = Path.Combine(webroot, "Images", student.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImagePath.CopyToAsync(fileStream);
                }

                student.Image = fileName;
            }

            // Update or add related CampusClass and CampusClassSection entities if needed
            var campusClass = await _context.CampusClasses
                .FirstOrDefaultAsync(cc => cc.CampusId == vm.CampusId && cc.ClassId == vm.ClassId);

            if (campusClass == null)
            {
                campusClass = new CampusClass
                {
                    CampusId = vm.CampusId,
                    ClassId = vm.ClassId,
                };
                _context.CampusClasses.Add(campusClass);
            }

            var campusClassSection = await _context.CampusClassSections
                .FirstOrDefaultAsync(ccs => ccs.CampusClassId == campusClass.CampusClassId && ccs.SectionId == vm.SectionId);

            if (campusClassSection == null)
            {
                campusClassSection = new CampusClassSection
                {
                    CampusClass = campusClass,
                    SectionId = vm.SectionId,
                };
                _context.CampusClassSections.Add(campusClassSection);
            }

            var studentCampusClassSection = await _context.StudentCampusClassSections
                .FirstOrDefaultAsync(sccs => sccs.StudentId == student.StudentId);

            if (studentCampusClassSection == null)
            {
                studentCampusClassSection = new StudentCampusClassSection
                {
                    CampusClassSection = campusClassSection,
                    Student = student,
                };
                _context.StudentCampusClassSections.Add(studentCampusClassSection);
            }
            else
            {
                studentCampusClassSection.CampusClassSection = campusClassSection;
            }

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(student);
        }


        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent([FromForm] StudentVM vm)
        {
            Student student = new Student
            {
                StudentFName = vm.StudentFName,
                StudentLName = vm.StudentLName,
                FatherName = vm.FatherName,
                MotherName = vm.MotherName,
                DateOfBirth = vm.DateOfBirth,
                BirthCertificateNumber = vm.BirthCertificateNumber,
                Address = vm.Address,
                GenderId = vm.GenderId
            };

            if (vm.ImagePath != null)
            {
                var webroot = _env.WebRootPath;
                var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(vm.ImagePath.FileName);
                var filePath = Path.Combine(webroot, "Images", fileName);

                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await vm.ImagePath.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                student.Image = fileName;
            }

            var campusClass = new CampusClass
            {
                CampusId = vm.CampusId,
                ClassId = vm.ClassId,
            };
            _context.Add(campusClass);

            var campusClassSection = new CampusClassSection
            {
                CampusClass = campusClass,
                CampusClassId = campusClass.CampusClassId,
                SectionId = vm.SectionId,
            };
            _context.Add(campusClassSection);

            var studentCampusClassSection = new StudentCampusClassSection
            {
                CampusClassSection = campusClassSection,
                StudentCampusClassSectionId = campusClassSection.CampusClassSectionId,
                Student = student,
                StudentId = student.StudentId
            };
            _context.Add(studentCampusClassSection);




            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }


        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
