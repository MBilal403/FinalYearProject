using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FYP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using FYP.Helpers;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace FYP.Controllers
{
    public class ProgramController : Controller
    {
        // GET: ProgramController
        public async Task<ActionResult> ViewPrograms()
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

        // GET: ProgramController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProgramController/Create
        public async Task<ActionResult> CreateProgram(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/Program/ProgramById/" + id;
                ViewBag.id = id.ToString();
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<ProgramModel> programs = JsonConvert.DeserializeObject<List<ProgramModel>>(jsonResponse)!;


                    /* foreach(var model in departments)
                      {
                      _logger.LogInformation(model.DepartId.ToString());
                      }*/
                    return View(programs);

                }
            }
            return View();

        }






        // POST: ProgramController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProgram(IFormCollection collection, int id)
        {
            try
            {
                string programName = collection["programname"];
                string programShortName = collection["programshortname"];
                string duration = collection["duration"];
                int number;

                number = int.Parse(duration);

                using (HttpClient httpClient = new HttpClient())
                {
                    // Send the request and post the response
                    var obj = new
                    {
                        ProgramName = programName,
                        ProgramShortName = programShortName,
                        Duration = number,
                        Status = false,
                        CreatedAt = DateTime.Now,
                        DepartFId = id
                    };
                    var jsonRequest = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(BaseURL.baseURl + "/Program/AddProgram", content);


                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("CreateProgram", new { id = id });
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            catch
            {
                return View();
            }


        }

        // GET: ProgramController/Edit/5
        public async Task<ActionResult> SearchDepartmentprogram(string searchQuery)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/department/SearchActiveDepartments?departName=" + searchQuery;
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

        // POST: ProgramController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult vbn(int id, IFormCollection collection)
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

        // GET: ProgramController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProgramController/Delete/5
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
