using FYP.Helpers;
using FYP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace FYP.Controllers
{
    public class AdminController : Controller
    {
        
        public FileManager _manager { get; set; }
        public AdminController(FileManager manager)
        {
           _manager = manager;
        }
        // GET: AdminController
        public async Task<ActionResult> ViewAdmins()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/auth/AdminUsers";
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<UserModel> Admins = JsonConvert.DeserializeObject<List<UserModel>>(jsonResponse)!;
                    /* foreach(var model in departments)
                      {
                      _logger.LogInformation(model.DepartId.ToString());
                      }*/
                    return View(Admins);

                }
            }
            return View();
            
        }

        // GET: AdminController/Details/5
        public ActionResult DetailAdmin(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult CreateAdmin()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAdmin(IFormCollection formCollection , IFormFile userimage)
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

                using (HttpClient httpClient = new HttpClient())
                {
                    // Set the authorization token in the request headers
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                    string apiURl = BaseURL.baseURl + "/student/checkemail?email=" + emailAddress;
                    // Send the request and get the response
                    HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.error = "Email Already registered";
                        return View();

                    }

                    string path = _manager.GetFilePath(userimage!)!;
                    string[] pathParts = path.Split(new[] { "wwwroot" }, StringSplitOptions.RemoveEmptyEntries);
                    string relativePath = pathParts[pathParts.Length - 1];
                    string pathWithBackslashes = relativePath.Replace('/', '\\');
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
                        Qualification = qualification,
                        UserImage = _manager.GetImageBytes(path),
                        ImagePath = pathWithBackslashes,
                        UserRole = "Admin",
                        Password = password,

                        CreatedAt = DateTime.Now,

                    };
                    using (HttpClient httpClient1 = new HttpClient())
                    {
                        // Send the request and post the response
                        var jsonRequest = JsonConvert.SerializeObject(dto);
                        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                        HttpResponseMessage response1 = await httpClient.PostAsync(BaseURL.baseURl + "/auth/UserSignup", content);

                        // Check if the response was successful
                        if (response1.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ViewAdmins");

                        }
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
