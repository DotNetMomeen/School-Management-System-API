using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class TeacherVM
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? TeacherImage { get; set; }
        [Display(Name ="Image")]
        public IFormFile? ImagePath { get; set; }
        public string Qualification { get; set; } = default!;
        public DateOnly JoinDate { get; set; } = default!;
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public virtual Gender? Gender { get; set; }
        public string? subjectStringify { get; set; }

        public List<Subject> subjectsList { get; set; } = new List<Subject>();
    }
}
