using FYP.Helpers;
using FYP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace FYP.Controllers
{
   
    public class AuthController : Controller
    {
        
        // GET: AuthController
        public ActionResult Index()
        {
            return View("Login");
        }

        // POST: AuthController/Login
        [HttpPost]
        public async  Task<ActionResult> Index(IFormCollection collection) // Login Page
        {
            string email = collection["LoginEmailInput"];
            email = email.ToLower().Trim();
            string password = collection["LoginPasswordInput"];
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewData["MyMessage"] = "Enter the Email and Password";
                return View("Login"); // redirect pr view bag or view data does not work
            } 
            else
            {
                try {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        
                        // Send the request and post the response
                        var loginData = new { Email = email, Password = password };
                        var jsonRequest = JsonConvert.SerializeObject(loginData);
                        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await httpClient.PostAsync(BaseURL.baseURl + "/auth/Login", content);

                        // Check if the response was successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the token from the response headers
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            var MyResponse = JsonConvert.DeserializeAnonymousType(jsonResponse, new
                            {
                                Token = null as string,
                                Response = new
                                {
                                    userId = (int?)null,
                                    email = null as string,
                                    userRole = null as string,
                                    ImagePath = null as string,
                                    fullname = null as string,
                                    userImage = (byte[])null!,
                                },
                                Message = null as string,
                                isSuccess = (bool?)null
                            });
                            // Create a ClaimsIdentity and set it as the User's identity
                            var claimsIdentity = new ClaimsIdentity(new Claim[]
                             {
                                new Claim(ClaimTypes.Name, MyResponse!.Response.userId.ToString()!), // Add any additional claims as needed
                                new Claim(ClaimTypes.Email, MyResponse.Response.email!), // Add any additional claims as needed
                                new Claim(ClaimTypes.Role, MyResponse.Response.userRole!) // Add any additional claims as needed
                             },CookieAuthenticationDefaults.AuthenticationScheme
                             );
                         
                            var principal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).ConfigureAwait(false);
                      

                            HttpContext.Session.SetString("UserId", MyResponse!.Response.userId.ToString()!);
                            HttpContext.Session.SetString("FullName", MyResponse.Response.fullname!);
                            HttpContext.Session.SetString("UserRole", MyResponse!.Response.userRole!);
                            HttpContext.Session.SetString("Email", MyResponse!.Response.email!);
                            HttpContext.Session.SetString("Token", MyResponse.Token!);
                            HttpContext.Session.SetString("ImagePath", MyResponse.Response.ImagePath!);
                            //  HttpContext.Session.Set("Image", MyResponse.Response.userImage);
                            // string token = tokenResponse.Token;
                            return RedirectToAction("index", "Dashboard");
                        }
                       
                    }
                    ViewData["MyMessage"] = "Invalid Crediantials";
                    return View("Login");
                }
                catch (Exception e)
                {
                    return View("Error");   
                }
               
            }
        }
        // GET: AuthController/Create
        public ActionResult ForgotPassword()
        {
            return View();
        }
        // POST: AuthController/Create
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(IFormCollection Form)
        {
            string email = Form["email"];
            
            string dob = Form["dateofbirth"];
            DateTime dateTime = DateTime.Parse(dob);

            email = email.ToLower().Trim();
           
            if (string.IsNullOrEmpty(email) ||  string.IsNullOrEmpty(dob))
            {
                ViewData["MyMessage"] = "Enter the Email and Password";
                return View(); // redirect pr view bag or view data does not work
            }
            else
            {

                ForgotPasswordDto passDto = new ForgotPasswordDto
                {
                
                    Email = email,
                    dateofbirth = dateTime
                };

                string jsonBody = JsonConvert.SerializeObject(passDto);

                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                try
                {
                    using (HttpClient _httpClient = new HttpClient())
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                        HttpResponseMessage response = await _httpClient.PostAsync(BaseURL.baseURl + "/auth/GetPasswordMail", content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            ViewBag.SuccessMessage = responseContent + "Check your mail";
                        }
                        else
                        {

                            ViewBag.ErrorMessage = "API request failed: ";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                }

                return View();
            }

        
        }
        // GET: AuthController/Signup
        public ActionResult Signup()
        {
            return View();
        }
        // GET: AuthController/verification
        public ActionResult Verification()
        {
            return View();
        }

        // POST: AuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(IFormCollection collection)
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

        // GET: AuthController/Edit/5
        public ActionResult EditDepartment(int id)
        {

            return View();
        }

        // POST: AuthController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(int id, IFormCollection collection)
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

        // GET: AuthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthController/Delete/5
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
        // GET: AuthController/Logout/
        public ActionResult Logout()
        {
            return RedirectToAction("index");
        }

    }
}
