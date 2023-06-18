namespace FYP.Models
{
    public class SemesterModel
    {
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public int ProgramFid { get; set; }
        public int CourseFId { get; set; }
        public int TeacherFid { get; set; }
        public DateTime TimeTable { get; set; }
        public ProgramModel? Program { get; set; }
        public CourseModel? Course { get;set; }
        public UserModel? User { get; set; }
    }

}
