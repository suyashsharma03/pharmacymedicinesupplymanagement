using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMedicineSupplyPortal.Models;

namespace PharmacyMedicineSupplyPortal.Controllers
{
    public class MRScheduleMeetController : Controller
    {
        string sd = null;
        string se = null;
        public IActionResult EnterDate()
        {
            if(TokenInfo.token==null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


        public IActionResult Index(IFormCollection form)
        {


            if (TokenInfo.token == null)
            {
                return RedirectToAction("Login", "Home");
            }

            sd = form["startDate"].ToString();
            se = sd.Replace('/', '-');
            TempData["Date1"] = se;

            return RedirectToAction("MrMeet", "MRScheduleMeet");
        }


        [HttpGet]
        public async Task<IActionResult> MrMeet()
        {

            if (TokenInfo.token == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string startDate = se;
            try
            {
                sd = TempData["Date1"].ToString();
                se = sd.Replace('/', '-');
                startDate = se;

            }
            catch (System.NullReferenceException e)
            {
                return RedirectToAction("Index", "MRScheduleMeet");
            }
            var MRMeetList = new List<RepSchedule>();
            using (var httpclient = new HttpClient())
            {
                HttpResponseMessage res = await httpclient.GetAsync("http://localhost:41851/api/RepSchedule/" + startDate);
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    MRMeetList = JsonConvert.DeserializeObject<List<RepSchedule>>(result);
                }
            }
            return View(MRMeetList);
        }
    }
}