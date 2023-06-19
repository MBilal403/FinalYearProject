namespace FYP.Models
{
    public class SPCourseModel
    {
        public int SPCourseId { get; set; }
        public int CourseFId { get; set; }
        public int UserFId { get; set; }
        public int SPFId { get; set; }
        public CourseModel? Course { get; set; }
        public UserModel? User { get; set; }
    }
}
