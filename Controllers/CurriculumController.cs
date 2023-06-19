using FYP.Models;
using FYP.Helpers;
using FYP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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
       /* public async Task<ActionResult> SemesterDetail(int id)
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
*/
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
        public async Task<ActionResult> ViewSemester(int id)
        {
            HttpContext.Session.SetString("SPFId", id.ToString()!);
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/SPCourse/Get/" + id;
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<SPCourseModel> semesters = JsonConvert.DeserializeObject<List<SPCourseModel>>(jsonResponse)!;
                    
                 
                    return View(semesters);

                }
            }


            return View();
        }

        // POST: CoursesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCourse(IFormCollection collection)
        {
            int SPId;
            var selectedCourseId = collection["course"];
            var selectedTeacherId = collection["teacher"];
            var SPid = HttpContext.Session.GetString("SPFId")!;
            if(SPid != null)
            {
                 SPId = int.Parse(SPid);

            }else
            {
                SPId = 0;
            }
            int course = int.Parse(selectedCourseId);
            int teacher = int.Parse(selectedTeacherId);



            SPCourseModel model = new SPCourseModel()
            {
                CourseFId = course,
                UserFId = teacher,
                SPFId = SPId

            };
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send the request and post the response
                 
                    var jsonRequest = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(BaseURL.baseURl + "/SPCourse/AddCourse", content);


                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewSemester", new { id = model.SPFId }); ;
                    }
                    return RedirectToAction(nameof(Index));
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoursesController/Delete/5
        public async Task<ActionResult> ViewSemesterList(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/SP/Get/"+id;
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.GetAsync(apiURl);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<SPModel> departments = JsonConvert.DeserializeObject<List<SPModel>>(jsonResponse)!;

                  
                    return View(departments);
                }
            }
            return View();
        }





        // POST: CoursesController/Delete/5
       
        public async Task<ActionResult> Delete(int id)
        {
            int SPId;
            var SPid = HttpContext.Session.GetString("SPFId")!;
            if (SPid != null)
            {
                SPId = int.Parse(SPid);

            }
            else
            {
                SPId = 0;
            }
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the authorization token in the request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                string apiURl = BaseURL.baseURl + "/SPCourse/Delete/" + id;
                // Send the request and get the response
                HttpResponseMessage response = await httpClient.DeleteAsync(apiURl);

                if (response.IsSuccessStatusCode)
                {
                   
                    return RedirectToAction("ViewSemester", new {id = SPId });
                }
            }
            return View();
        }
    }
}
