using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _Configure;
        private readonly string apiBaseUrl;
        public EmployeeController(IConfiguration configuration)
        {
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<EmployeeVM> reservationList = new List<EmployeeVM>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Employee"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<EmployeeVM>>(apiResponse);
                }
            }
            return View(reservationList);
        }
    }
}
