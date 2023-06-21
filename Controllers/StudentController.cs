using FYP.Helpers;
using FYP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FYP.Controllers
{
    public class StudentController : Controller
    {
        public FileManager _manager { get; set; }
        public StudentController(FileManager manager)
        {
            _manager = manager;
        }
        // GET: StudentController
        public async Task<ActionResult> ViewStudents()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/Student/Students";
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<StudentModel> Students = JsonConvert.DeserializeObject<List<StudentModel>>(jsonResponse)!;
                    /* foreach(var model in departments)
                      {
                      _logger.LogInformation(model.DepartId.ToString());
                      }*/
                    return View(Students);
                }
            }
            return View();

        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult CreateStudent()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStudent(IFormCollection form, IFormFile userimage)
        {
            try
            {
                // Retrieve form values from the IFormCollection
                string fullName = form["fullname"];
                string emailAddress = form["emailaddress"];
                string fatherName = form["fathername"];
                string rollNumber = form["rollnumber"];
                string gender = form["gender"];
                string dateOfBirth = form["dateofbirth"];
                string address = form["address"];
                string contactNumber = form["contactnumber"];
                string city = form["city"];
                string session = form["session"];
                string department = form["department"];
                string program = form["program"];
                string semester = form["semester"];
                // Perform validation

                string imagepath = _manager.GetFilePath(userimage!)!;

                string[] pathParts = imagepath.Split(new[] { "wwwroot" }, StringSplitOptions.RemoveEmptyEntries);

                string relativePath = pathParts[pathParts.Length - 1];

                string pathWithBackslashes = relativePath.Replace('/', '\\');

                int passwordLength = 10;
                string password = PasswordGenerator.GeneratePassword(passwordLength);

                var dto = new StudentModel()
                {
                    FullName = fullName,
                    Email = emailAddress,
                    RollNumber = rollNumber,
                    FatherName = fatherName,
                    Gender = gender,
                    DateOfBirth = DateTime.Parse(dateOfBirth),
                    Address = address,
                    ContactNumber = contactNumber,
                    City = city,
                    Image = _manager.GetImageBytes(imagepath),
                    ImagePath = pathWithBackslashes,
                    Session = session,
                    Password = password,
                    Department= department,
                    Program = program,
                    Semester = int.Parse(semester),
                    CreatedAt = DateTime.Now,

                };
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send the request and post the response
                    var jsonRequest = JsonConvert.SerializeObject(dto);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(BaseURL.baseURl + "/Student/Register", content);

                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewStudents");

                    }
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
