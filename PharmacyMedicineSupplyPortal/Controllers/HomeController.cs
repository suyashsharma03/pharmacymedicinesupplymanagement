using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PharmacyMedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static string token;
        private IConfiguration configuration;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(HomeController));
        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            configuration = _configuration;
        }
        public ActionResult Login()
        {
            _log4net.Info("Pensioner is logging in");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login cred)
        {
            _log4net.Info("Post Login is called");
            Login loginCred = new Login();

            using (var httpClient = new HttpClient())
            {
                //trying to convert json username and password to string
                StringContent content = new StringContent(JsonConvert.SerializeObject(cred), Encoding.UTF8, "application/json");

                //calling jwt or authorization api and passing username and password as parameters
                using (var response = await httpClient.PostAsync("http://localhost:54859/api/Auth", content))
                {
                    //checking the response code
                    if (!response.IsSuccessStatusCode)
                    {
                        _log4net.Info("Login failed");
                        ViewBag.Message = "Please Enter valid credentials";
                        return View("Login");
                    }
                    _log4net.Info("Login Successful and token generated");
                    string strtoken = await response.Content.ReadAsStringAsync(); //if response code is succesful storingtoken


                    loginCred = JsonConvert.DeserializeObject<Login>(strtoken);
                    string userName = cred.Username;
                    TokenInfo.token = token = strtoken; //token is a static variable of TokenInfo class storing token value in it
                }
            }

            return RedirectToAction("EnterDate", "MRScheduleMeet", new { name = token });
        }

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }
        public ActionResult Logout()
        {
            TokenInfo.token = null;
            return RedirectToAction("Login", "Home", new { name = "" });
        }
        public IActionResult Index()
        {
            return View();
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
