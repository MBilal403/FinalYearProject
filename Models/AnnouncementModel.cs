namespace FYP.Models
{
    public class AnnouncementModel
    {
       
            public int AnnouncementId { get; set; }
            public string? Title { get; set; }
            public string? Message { get; set; }
            public string? UserName { get; set; }
            public string? UserRole { get; set; }
            public DateTime CreatedAt { get; set; }
            public string? UserEmail { get; set; }
       
    }
}
