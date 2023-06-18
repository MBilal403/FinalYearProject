using FYP.Helpers;
using FYP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FYP.Controllers
{
    public class CourseController : Controller
    {
        // GET: CourseController GetAllCourses
        public async  Task<ActionResult> ViewCourses()
        {

            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/Course/GetAllCourses";
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<CourseModel> departments = JsonConvert.DeserializeObject<List<CourseModel>>(jsonResponse)!;
                    /* foreach(var model in departments)
                      {
                      _logger.LogInformation(model.DepartId.ToString());
                      }*/
                    return View(departments);

                }
            }
            return View();

        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public ActionResult CreateCourse()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCourse(IFormCollection collection)
        {
            try
            {
                string code = collection["code"];
                string title = collection["title"];
                string department = collection["department"];
                string credithour = collection["credithour"];
                int number,number2;

                number = int.Parse(department);
                number2 = int.Parse(credithour);

                using (HttpClient httpClient = new HttpClient())
                {
                    // Send the request and post the response
                    var obj = new
                    {
                        CourseCode = code,
                        CourseTitle = title,
                        CourseCreditHours = number2,
                        DepartmentFId = number,
                      
                    };
                    var jsonRequest = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(BaseURL.baseURl + "/Course/AddCourse", content);


                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewCourses");
                    }
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public ActionResult EditCourse(int id)
        {
            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(int id, IFormCollection collection)
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

        // GET: CourseController/Delete/5
        public async Task<ActionResult> DeleteCourse(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/Course/DeleteCourse/" + id;
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.DeleteAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewCourses");
                }
            }
            return View();

        }

        // POST: CourseController/Delete/5
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
