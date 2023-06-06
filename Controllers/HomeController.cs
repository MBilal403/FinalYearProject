using FYP.Helpers;
using FYP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FYP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ContactUs()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(IFormCollection collection)
        {
            
                string name = collection["name"];
                string email = collection["email"];
                string message = collection["message"];

                 MailDto mailDto = new MailDto
                    {
                        Name = name,
                        Email = email,
                        Message = message
                    };

                    string jsonBody = JsonConvert.SerializeObject(mailDto);

                    HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    try
                    {
                        using(HttpClient _httpClient = new HttpClient()) {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                        HttpResponseMessage response = await _httpClient.PostAsync(BaseURL.baseURl + "/home/sendmail", content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            ViewBag.SuccessMessage = "Response: " + responseContent +" Thanks";
                        }
                        else
                        {
                          
                            ViewBag.ErrorMessage = "API request failed: " ;
                        }
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                    }

                    return View();
        }
        [HttpPost]
        public async Task<IActionResult> Annoncement(IFormCollection collection)
        {

            string name = collection["name"];
            string email = collection["email"];
            string message = collection["message"];

            MailDto mailDto = new MailDto
            {
                Name = name,
                Email = email,
                Message = message
            };

            string jsonBody = JsonConvert.SerializeObject(mailDto);

            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient _httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token")!);

                    HttpResponseMessage response = await _httpClient.PostAsync(BaseURL.baseURl + "/home/sendmail", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        ViewBag.SuccessMessage = "Response: " + responseContent + " Thanks";
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}