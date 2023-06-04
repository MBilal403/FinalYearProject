using FYP.Helpers;
using FYP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace FYP.Controllers
{
    public class TeacherController : Controller
    {
        public FileManager _manager { get; set; }
        public TeacherController(FileManager manager)
        {
            _manager = manager;
        }
        // GET: TeacherController
        public async  Task<ActionResult> ViewTeachers()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/auth/TeacherUsers";
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<UserModel> Teachers = JsonConvert.DeserializeObject<List<UserModel>>(jsonResponse)!;
                    /* foreach(var model in departments)
                      {
                      _logger.LogInformation(model.DepartId.ToString());
                      }*/
                    return View(Teachers);

                }
            }
        
            return View();
        }

        // GET: TeacherController/Details/5
        public ActionResult DetailTeacher(int id)
        {
            return View();
        }

        // GET: TeacherController/Create
        public ActionResult CreateTeacher()
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTeacher(IFormCollection formCollection,IFormFile userimage, IFormFile resume )
        {
            try
            {
                // Retrieve form values from the IFormCollection
                string fullName = formCollection["fullname"];
                string emailAddress = formCollection["emailaddress"];
                string gender = formCollection["gender"];
                string dateOfBirth = formCollection["dateofbirth"];
                string address = formCollection["address"];
                string contactNumber = formCollection["contactnumber"];
                string city = formCollection["city"];
                string qualification = formCollection["qualification"];
                // Perform validation
                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(gender) ||
                    string.IsNullOrEmpty(dateOfBirth) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(contactNumber) ||
                    string.IsNullOrEmpty(city) || string.IsNullOrEmpty(qualification) || userimage == null)
                {
                    // Handle validation error (e.g., display error message to the user)
                    ModelState.AddModelError(string.Empty, "Please fill in all the required fields.");
                    return View(); // Return the view to show the form again with the error messages
                }
                string imagepath = _manager.GetFilePath(userimage!)!;
                string resumepath = _manager.GetFilePath(resume!)!;
                string[] pathParts = imagepath.Split(new[] { "wwwroot" }, StringSplitOptions.RemoveEmptyEntries);
                string[] resumepathParts = resumepath.Split(new[] { "wwwroot" }, StringSplitOptions.RemoveEmptyEntries);
                string relativePath = pathParts[pathParts.Length - 1];
                string relativePathresume = resumepathParts[pathParts.Length - 1];
                string pathWithBackslashes = relativePath.Replace('/', '\\');
                string resumepathWithBackslashes = relativePathresume.Replace('/', '\\');
                int passwordLength = 10;
                string password = PasswordGenerator.GeneratePassword(passwordLength);

                var dto = new UserModel()
                {
                    FullName = fullName,
                    Email = emailAddress,
                    Gender = gender,
                    DateOfBirth = DateTime.Parse(dateOfBirth),
                    Address = address,
                    ContactNumber = contactNumber,
                    City = city,
                    Resume = _manager.GetImageBytes(resumepath),
                    Qualification = qualification,
                    UserImage = _manager.GetImageBytes(imagepath),
                    ImagePath = pathWithBackslashes,
                    ResumePath = resumepathWithBackslashes,
                    UserRole = "Teacher",
                    Password = password,

                    CreatedAt = DateTime.Now,

                };
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send the request and post the response
                    var jsonRequest = JsonConvert.SerializeObject(dto);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(BaseURL.baseURl + "/auth/UserSignup", content);

                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewTeachers");

                    }
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Edit/5
        public ActionResult Edit(int id) // done by admin
        {
            return View();
        }

        // POST: TeacherController/Edit/5
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

        // GET: TeacherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeacherController/Delete/5
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
