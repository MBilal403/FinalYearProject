using FYP.Models;
using Microsoft.AspNetCore.Hosting;

namespace FYP.Helpers
{
    public class FileManager
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileManager(IWebHostEnvironment webHostEnvironment) { 
            _webHostEnvironment = webHostEnvironment;
           
        }

        public string? GetFilePath(IFormFile file) {
            string? uploadsFolder = null;
            //string? myPath = null;
            if (file == null || file.Length <= 0)
            {
                return null;
            }

            // Generate a unique filename
            // Get the uploads folder path
            string fileName = Guid.NewGuid().ToString("N").Substring(0, 10) + Path.GetExtension(file.FileName);
           if(Path.GetExtension(file.FileName) == ".pdf")
            {
                uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UserResume");
            }
            else
            {

             uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UserImages");
            }
             //   myPath = "/Images/" + fileName;
          
            // Create the uploads folder if it doesn't exist
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
        
            // Combine the uploads folder path with the file name
            string filePath = Path.Combine(uploadsFolder, fileName);
            // Save the file to the server
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            byte[] filebyte = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.WriteAllBytes(filePath, filebyte);
            
            return filePath;

        }
        public byte[] GetImageBytes(string path)
        {
            // Assuming you have a default image stored in your project or as a resource
            // Load the default image file or generate a default image byte array
            // Option 1: Load default image file from disk
            // string defaultImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "DefaultImage.jpg");
           // string defaultImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", path);
            byte[] defaultImageBytes = File.ReadAllBytes(path);
            return defaultImageBytes;
        }





    }
}
