using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? TeacherImage { get; set; }
        public string Qualification { get; set; } = default!;
        public DateOnly JoinDate { get; set; } = default!;
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
        }
}
