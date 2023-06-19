namespace FYP.Models
{
    public class SPModel
    {
        public int SPId { get; set; }
        public int ProgramFId { get; set; }
        public int SemesterFId { get; set; }
        public CourseModel? Course { get; set; }
        public UserModel? User { get; set; }
    }
}
