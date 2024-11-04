namespace SchoolManagementSystem.Models
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; } = default!;
        public  TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public virtual List<Campus> Campuses { get; set; } = new List<Campus>();
    }
}
