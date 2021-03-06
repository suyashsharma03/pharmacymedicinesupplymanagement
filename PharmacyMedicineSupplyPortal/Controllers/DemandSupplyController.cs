using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMedicineSupplyPortal.Models;
using PharmacyMedicineSupplyPortal.Repository;

namespace PharmacyMedicineSupplyPortal.Controllers
{
    public class DemandSupplyController : Controller
    {
        private IDemands repo;
        private ISupplies supplyrepo;
        private static int forViewBagFlag;
        public DemandSupplyController(IDemands repo, ISupplies supplyrepo)
        {
            this.repo = repo;
            this.supplyrepo = supplyrepo;
        }
        public async Task<IActionResult> Index()
        {

            if (TokenInfo.token == null)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                var stock = new List<MedicineStock>();

                using (var httpclient = new HttpClient())
                {
                    HttpResponseMessage res = await httpclient.GetAsync("http://localhost:40486/api/medicinestock");
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        stock = JsonConvert.DeserializeObject<List<MedicineStock>>(result);
                    }
                }
                if (stock.Count == 0)
                {
                    return RedirectToAction("Index", "DemandSupply");
                }
                var list = new List<MedicineDemand>();
                foreach (var med in stock)
                {
                    list.Add(new MedicineDemand { Medicine = med.MedicineName, DemandCount = 0 });
                }
                ViewBag.Demands = list;
                if (forViewBagFlag == 1)
                    ViewBag.Message = "Demand Count Exceeds Stock";
                else
                    ViewBag.Message = null;

                return View();
            }
            catch(Exception e)
            {
                ViewBag.Message="Error message"+e;
                return RedirectToAction("Index","DemandSupply");
            }
        }

        [HttpPost]
        public IActionResult Add(MedicineDemand meds)
        {
            try
            {
                if (meds == null)
                {
                    return RedirectToAction("Index", "DemandSupply");
                }
                Demands newdemand = new Demands()
                {
                    Medicine = meds.Medicine,
                    Demand = meds.DemandCount
                };
                int res = repo.AddDemand(newdemand);
                if (res > 0)
                {
                    return RedirectToAction("AddSupply", newdemand);
                }
                //ViewBag.DemandId = res;
                return RedirectToAction("Index", "DemandSupply");
            }
            catch(Exception e)
            {
                ViewBag.ex = e;
                return RedirectToAction("Index", "DemandSupply");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddSupply(Demands med)
        {
            int id = 0;
            if (TokenInfo.token == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (med==null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var distributionOfStock = new List<PharmacyMedicineSupply>();

                using (var httpclient = new HttpClient())
                {
                    HttpResponseMessage res = await httpclient.GetAsync("http://localhost:40486/api/pharmacymedicine/getsupplies/" + med.Medicine + "/" + med.Demand);
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        distributionOfStock = JsonConvert.DeserializeObject<List<PharmacyMedicineSupply>>(result);
                    }
                }
                if (distributionOfStock.Count == 0)
                {
                    forViewBagFlag = 1;
                    return RedirectToAction("Index", "DemandSupply");
                }
                else
                    forViewBagFlag = 0;
                foreach (var supply in distributionOfStock)
                {
                    id = supplyrepo.AddSupply(new Supplies { PharmacyName = supply.PharmacyName, MedicineName = supply.MedicineName, SupplyCount = supply.SupplyCount });
                }
                ViewBag.SupplyId = id;
                if(TokenInfo.token != null)
                    return View(distributionOfStock);
                else
                    return RedirectToAction("Login", "Home");
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "DemandSupply");
            }
            
        }
       public ActionResult Delete(int id)
        {
            supplyrepo.DeleteSupply(id);
            return RedirectToAction("Index", "DemandSupply");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                supplyrepo.DeleteSupply(id);
                //repo.DeleteDemand(id);
                return RedirectToAction("Index", "DemandSupply");
            }
            catch
            {
                return RedirectToAction("Index", "DemandSupply");
            }
        }
    }

}

