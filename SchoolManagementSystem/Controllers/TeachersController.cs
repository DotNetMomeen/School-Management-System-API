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
    public class TeachersController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        private readonly IWebHostEnvironment _env;

        public TeachersController(SchoolDbContext _context, IWebHostEnvironment _env)
        {
            this._context = _context;
            this._env = _env;
        }

        [HttpGet]
        [Route("GetSubject")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubject()
        {
            return await _context.Subjects.ToListAsync();
        }

        // GET: api/Teachers
        [HttpGet]
        [Route("GetTeacher")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherVM>>> GetTeacherSubject()
        {
            List<TeacherVM> TeacherSubjectList = new List<TeacherVM>();
            var allTeacher = _context.Teachers.ToList();
            foreach (var teacher in allTeacher)
            {
                var subjectList = _context.TeachersSubject.Where(x => x.TeacherId == teacher.TeacherId).Select(x => new Subject
                {
                    SubjectId = x.SubjectId,
                    SubjectName = x.Subject.SubjectName
                }).ToList();

                TeacherSubjectList.Add(new TeacherVM
                {
                    TeacherId = teacher.TeacherId,
                    TeacherName = teacher.TeacherName,
                    DateOfBirth = teacher.DateOfBirth,
                    Phone = teacher.Phone,
                    TeacherImage = teacher.TeacherImage,
                    Qualification = teacher.Qualification,
                    JoinDate = teacher.JoinDate,
                    GenderId = teacher.GenderId,
                    subjectsList = subjectList.ToList()

                });
            }
            return TeacherSubjectList;
        }

       
        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<TeacherSubject>> UpdateTeacherSubject([FromForm] TeacherVM vm)
        {
            var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);

            Teacher teacher = _context.Teachers.Find(vm.TeacherId);
            teacher.TeacherName = vm.TeacherName;
            teacher.DateOfBirth = vm.DateOfBirth;
            teacher.TeacherImage= vm.TeacherImage;
            teacher.Phone = vm.Phone;
            teacher.Qualification = vm.Qualification;
            teacher.JoinDate = teacher.JoinDate;
            teacher.GenderId = teacher.GenderId;


            if (vm.ImagePath != null)
            {
                var webroot = _env.WebRootPath;
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImagePath.FileName);
                var filePath = Path.Combine(webroot, "Images", fileName);

                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await vm.ImagePath.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                teacher.TeacherImage = fileName;
            }

            //Delete existing spots
            var existingSubject = _context.TeachersSubject.Where(x => x.TeacherId == teacher.TeacherId).ToList();
            foreach (var item in existingSubject)
            {
                _context.TeachersSubject.Remove(item);
            }

            //Add newly added spots
            foreach (var item in subjectItems)
            {
                var teacherSubject = new TeacherSubject
                {
                    TeacherId = teacher.TeacherId,
                    SubjectId = item.SubjectId

                };
                _context.Add(teacherSubject);
            }

            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<TeacherSubject>> PostTeacherSubject([FromForm] TeacherVM vm)
        {
            var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);

            Teacher teacher = new Teacher
            {
                TeacherName = vm.TeacherName,
                DateOfBirth = vm.DateOfBirth,
                Phone = vm.Phone,
                Qualification = vm.Qualification,
                JoinDate = vm.JoinDate,
                GenderId = vm.GenderId
            };
            //For Image
            if (vm.ImagePath != null)
            {
                var webroot = _env.WebRootPath;
                var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(vm.ImagePath.FileName);
                var filePath = Path.Combine(webroot, "Images", fileName);

                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await vm.ImagePath.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                teacher.TeacherImage = fileName;
            }
            //for SkillList

            foreach (var item in subjectItems)
            {
                var teacherSubject = new TeacherSubject
                {
                    Teacher = teacher,
                    TeacherId = teacher.TeacherId,
                    SubjectId = item.SubjectId
                };
                _context.Add(teacherSubject);
            }
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }


        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult<TeacherSubject>> DeleteTeacherSubject(int id)
        {
            Teacher teacher = _context.Teachers.Find(id);

            var existingSubject = _context.TeachersSubject.Where(x => x.TeacherId == teacher.TeacherId).ToList();
            foreach (var item in existingSubject)
            {
                _context.TeachersSubject.Remove(item);
            }
            _context.Entry(teacher).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return Ok(teacher);
        }
    }
}
