namespace FYP.Models
{
    public class DepartmentModel
    {
            public int DepartId { get; set; }
            public string? DepartName { get; set; }
            public string? InchargeName { get; set; }
            public string? AdminName { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool Status { get; set; }
    }
}
