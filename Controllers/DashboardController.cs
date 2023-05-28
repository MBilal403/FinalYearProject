
using EduSpaceAPI.Helpers;
using EduSpaceAPI.Models;
using FYP.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;

namespace FYP.Controllers
{
    public class DashboardController : Controller
    {
        private string BaseUrl = "https://localhost:7031";
        // GET: DashboardController
        [Authorize(Roles = "SuperAdmin,Teacher,Admin")]
        public async Task<ActionResult> Index()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("Token");
                    // Set the authorization token in the request headers
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token );
                    string id = HttpContext.Session.GetString("UserId");
                    string apiURl = BaseUrl + "/auth/UserById/" + int.Parse(HttpContext.Session.GetString("UserId")!);
                    // Send the request and get the response
                    HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Process the response as needed
                        // For example, deserialize the JSON response into an object
                        MyResponse<UserModel> responseObject = JsonConvert.DeserializeObject<MyResponse<UserModel>>(jsonResponse);
                        ViewData["ImagePath"] = responseObject!.Response!.ImagePath!;
                        // Use the responseObject as needed
                    }

                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Handle the error appropriately
                }
            }

            return View("MainPage");
        }

        // GET: DashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashboardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: DashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashboardController/Edit/5
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

        // GET: DashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardController/Delete/5
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
