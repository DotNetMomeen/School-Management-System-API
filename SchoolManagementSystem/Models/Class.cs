namespace SchoolManagementSystem.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = default!;
        public virtual ICollection<CampusClass> CampusClass { get; set; } = new List<CampusClass>();
        //public virtual ICollection<StudentCampusClassSection> StudentCampusClassSections { get; set; } = new List<StudentCampusClassSection>();
        public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
