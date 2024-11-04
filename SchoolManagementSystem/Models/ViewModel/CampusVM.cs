using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class CampusVM
    {
        public int CampusId { get; set; }
        public string Name { get; set; }
        [ForeignKey("Curriculum")]
        public int CurriculumId { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public string? ClassStringify { get; set; }

        public List<Class> classList { get; set;} = new List<Class>();
    }
}
