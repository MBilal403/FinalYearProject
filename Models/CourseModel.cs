namespace FYP.Models
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseTitle { get; set; }
        public string? DepartName { get; set; }
        public int CourseCreditHours { get; set; }
        public int DepartmentFId { get; set; }
    }
}
