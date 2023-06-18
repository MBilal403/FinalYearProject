using FYP.Helpers;
using FYP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FYP.Controllers
{
    public class CurriculumController : Controller
    {
        // GET: CoursesController
        public async Task<ActionResult> ViewCurriculum()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/department/GetDepartmentPrograms";
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


        // GET: CoursesController/Details/5
        public async Task<ActionResult> SemesterDetail(int id)
        {
            List<UserModel> users = new List<UserModel>();
            List<ProgramModel> programs = new List<ProgramModel>();
            List<CourseModel> courses = new List<CourseModel>();
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/Semester/GetByProgramId/" + id;
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<SemesterModel> semesters = JsonConvert.DeserializeObject<List<SemesterModel>>(jsonResponse)!;
                    var re =  semesters.OrderBy(t => t.SemesterNo);


                    foreach (var model in semesters)
                    {
                        users.Add(model.User!);
                        programs.Add(model.Program!);
                        courses.Add(model.Course!);
                    }
                    ViewBag.user = users;
                    ViewBag.program = programs;
                    ViewBag.course = courses;
                    return View(semesters);

                }
            }
            return View();
        }

        // GET: CoursesController/Create
        public async Task<ActionResult> CreateSemester(int id , int id1)
        {
         
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/Semester/GetByProgramId/" + id1;
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<SemesterModel> semesters = JsonConvert.DeserializeObject<List<SemesterModel>>(jsonResponse)!;
                    var semester =  semesters.Where(t => t.SemesterId == id);
                    if (id == null)
                    {

                    }

                    return View(semester);

                }
            }
            return View();



        }

        // POST: CoursesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSemester(IFormCollection collection)
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

        // GET: CoursesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoursesController/Edit/5
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

        // GET: CoursesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoursesController/Delete/5
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
