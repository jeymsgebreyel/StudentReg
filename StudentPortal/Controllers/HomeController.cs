using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentPortal.Models;
using StudentPortal.ViewModel;
using StudentRegCore;
using System.Diagnostics;
using System.Text;

namespace StudentPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string baseUrl = "https://localhost:7181/api";
        public HomeController(ILogger<HomeController> logger)
        {

            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpRequestMessage request;
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                
                request = new HttpRequestMessage(HttpMethod.Get,baseUrl+"/Students");

                response = await client.SendAsync(request);
            }
            var dataString = await response.Content.ReadAsStringAsync();
            StudentViewModel vm = new StudentViewModel();
            vm.StudentList = JsonConvert.DeserializeObject<List<StudentDTO>>(dataString);
            return View(vm);
            //return await ReadRestApiResponse<T>(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpRequestMessage request;
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {

                request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/Students/"+id);

                response = await client.SendAsync(request);
            }
            var dataString = await response.Content.ReadAsStringAsync();
            StudentViewModel vm = new StudentViewModel();
            vm.Student = JsonConvert.DeserializeObject<StudentDTO>(dataString);
            return View(vm);
        }

        public IActionResult Register()
        {
            StudentViewModel vm = new StudentViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(StudentViewModel vm)
        {
            HttpRequestMessage request;
            HttpResponseMessage response;
            string jsonData = JsonConvert.SerializeObject(vm.NewStudent);
            HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {

                request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/Students");
                request.Content = httpContent;
                response = await client.SendAsync(request);
            }

            return RedirectToAction(nameof(Index));
            //return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}