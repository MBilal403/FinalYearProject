using FYP.Helpers;
using FYP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FYP.Controllers
{
    public class DepartmentController : Controller
    {
        private ILogger<DepartmentController> _logger;
        public DepartmentController(ILogger<DepartmentController> logger)
        {
            _logger = logger;
        }
        
        // GET: DeaprtmentController
        public async Task<ActionResult> ViewDepartments()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/department/departments";
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<DepartmentModel> departments = JsonConvert.DeserializeObject<List<DepartmentModel>>(jsonResponse)!;
                  /* foreach(var model in departments)
                    {
                    _logger.LogInformation(model.DepartId.ToString());
                    }*/
                    return View(departments);

                }
            }
                return View();
        }


        // GET: DeaprtmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: DeaprtmentController/Create
        public async Task<ActionResult> ViewDepartments(IFormCollection collection)
        {
            string departname = collection["departName"];
            departname = departname.ToLower().Trim();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send the request and post the response
                    var obj = new { DepartName = departname,
                        CreatedAt = DateTime.Now,
                        Status  = true
                    };
                    var jsonRequest = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(BaseURL.baseURl + "/department/Create", content);


                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpClient httpClient1 = new HttpClient())
                        {
                            // Set the authorization token in the request headers
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                            string apiURl = BaseURL.baseURl + "/department/departments";
                            // Send the request and get the response
                            HttpResponseMessage response1 = await httpClient1.GetAsync(apiURl);
                            if (response.IsSuccessStatusCode)
                            {
                                // Read the response content
                                string jsonResponse = await response1.Content.ReadAsStringAsync();
                                List<DepartmentModel> departments = JsonConvert.DeserializeObject<List<DepartmentModel>>(jsonResponse)!;
                                
                                return View(departments);

                            }
                        }
                        /*       // Read the token from the response headers
                               string jsonResponse = await response.Content.ReadAsStringAsync();
                               var MyResponse = JsonConvert.DeserializeAnonymousType(jsonResponse, new
                               {
                                   Token = null as string,
                                   Response = new
                                   {
                                       DepartName = "",
                                       InchargeName = "",
                                       AdminName = "",
                                       CreatedAt = default(DateTime),
                                       Status = false
                                   },
                                   Message = null as string,
                                   isSuccess = (bool?)null
                               });*/
                    }
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return   View("ViewDepartments");
        }

        // POST: DeaprtmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {


                return RedirectToAction(nameof(ViewDepartments));
            }
            catch
            {
                return View();
            }
        }

        // GET: DeaprtmentController/Search/5
        public async Task<ActionResult> Search(string searchQuery)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/department/DepartmentsWithName?departName=" + searchQuery;
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<DepartmentModel> departments = JsonConvert.DeserializeObject<List<DepartmentModel>>(jsonResponse)!;

                    return View(departments);

                }
                return View();
            }
        }

        // POST: DeaprtmentController/Edit/5
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

        // GET: DepartmentController/Delete/5
        public ActionResult DeleteDepartment(int id)
        {

            return View();
        }

        // POST: DeaprtmentController/Delete/5
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
