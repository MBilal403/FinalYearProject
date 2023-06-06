namespace FYP.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Department { get; set; }
        public string? Program { get; set; }
        public string? FatherName { get; set; }
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Session { get; set; }
        public string? ContactNumber { get; set; }
        public int Semester { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? RollNumber { get; set; }
        public string? Gender { get; set; }
    }
}
