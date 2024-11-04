namespace SchoolManagementSystem.Models.ViewModel
{
    public class ShiftVM
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; } = default!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

    }
}
