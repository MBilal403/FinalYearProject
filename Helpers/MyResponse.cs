using System.Security.Cryptography;

namespace EduSpaceAPI.Helpers
{
    public class MyResponse<T>
    {
        public T? Response { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }
}
