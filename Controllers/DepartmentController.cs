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
            departname = departname.ToUpper().Trim();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send the request and post the response
                    var obj = new { DepartName = departname,
                        CreatedAt = DateTime.Now,
                        Status  = false
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


        // GET: DeaprtmentController/EditDepartment/5
        public async Task<ActionResult> EditDepartment(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/department/DepartmentById/" + id;
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<DepartmentModel> department = JsonConvert.DeserializeObject<List<DepartmentModel>>(jsonResponse)!;

                    return View(department[0]);

                }
                return View();
            }

        }



        // POST: DeaprtmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDepartment(int id, IFormCollection collection)
        {
                string departname = collection["departmentname"];
                departname = departname.ToUpper().Trim();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send the request and post the response
                    var obj = new
                    {
                        DepartName = departname
                    };
                    var jsonRequest = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PutAsync(BaseURL.baseURl + "/department/UpdateDepartName/" + id, content);


                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewDepartments");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }

    }

        // GET: DepartmentController/
        public async Task<ActionResult> ViewActiveDepartment()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/department/ActiveDepartments";
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
        // GET: DepartmentController/Delete/
        public async Task<ActionResult> ViewNonActiveDepartment()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/department/NonActiveDepartments";
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
        // POST: DeaprtmentController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Setting(int id)
        {

            try
            {
                // Send the request and post the response
             
                using (HttpClient httpClient = new HttpClient())
                {
                    // Set the authorization token in the request headers
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                    string apiURl = BaseURL.baseURl + "/department/DepartmentById/" + id;
                    // Send the request and get the response
                    HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<DepartmentModel> department = JsonConvert.DeserializeObject<List<DepartmentModel>>(jsonResponse)!;

                        return View(department[0]);

                    }
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // POST: DeaprtmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Setting(int id, IFormCollection collection)
        {
            string incharge = collection["incharge"];
            string admin = collection["admin"];
           string inchargename = incharge.Trim();
            string adminname = admin.Trim();
            try
            {
                var obj = new
                {
                    AdminName = adminname,
                    InchargeName = inchargename
                };
                using (HttpClient httpClient = new HttpClient())
                {
                    var jsonRequest = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PutAsync(BaseURL.baseURl + "/department/UpdateInchargeAndAdminNames/" + id, content);


                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewDepartments");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
