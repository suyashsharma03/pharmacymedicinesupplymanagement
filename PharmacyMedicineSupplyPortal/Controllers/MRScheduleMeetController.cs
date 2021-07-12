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
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MRScheduleMeetController));
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
            if (DateTime.Parse(startDate).DayOfWeek == 0)
                ViewBag.Message = "Date Provided is Sunday. No Schedule to show";
            else if (MRMeetList == null || MRMeetList.Count < 1)
                ViewBag.Message = "No Schedule on the date provided";
            else
                ViewBag.Message = null;
            return View(MRMeetList);
        }
    }
}