using MedicineStockSupply.Model;
using MedicineStockSupply.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicineStockSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyMedicineController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PharmacyMedicineController));
        private IMedicineSupply _supply;
        private static readonly Random _random = new Random();

        static List<string> Pharmacies = new List<string>()
        { "Max Pharmacy", "Appolo Pharmacy", "Synergy Pharmacy", "GoodWill Pharmacy" };

        static List<PharmacyMedicineSupply> pharmacyMedicines = new List<PharmacyMedicineSupply>();

        public PharmacyMedicineController(IMedicineSupply supply)
        {
            _supply = supply;
        }

        // GET: api/<PharmacyMedicineController>
        [HttpGet]
        public IActionResult GetPharmacies()
        {
            if (_supply.GetAllPharmacies() != null)
                return Ok(_supply.GetAllPharmacies());
            return BadRequest("No Pharmacies");
        }

        // GET api/<PharmacyMedicineController>
        [HttpGet]
        [Route("GetSupplies/{medicineName}/{count}")]
        public async Task<IActionResult> GetSupplies(string medicineName, int count)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync("http://localhost:40486/api/medicinestock/" + medicineName);
                    //var medicines = await response.Content.ReadAsAsync<MedicineStock>(new[] { new JsonMediaTypeFormatter() });
                    var result = response.Content.ReadAsStringAsync().Result;
                    var medicines = JsonConvert.DeserializeObject<MedicineStock>(result);

                    if (medicines.NumberOfTabletsInStock >= count)
                        pharmacyMedicines.Add(new PharmacyMedicineSupply()
                        {
                            MedicineName = medicineName,
                            PharmacyName = Pharmacies[_random.Next(0, 3)],
                            SupplyCount = count
                        });
                }
            }
            catch(Exception e)
            {
                //logging
                _log4net.Error(e.Message);
                return BadRequest(pharmacyMedicines.ToList());
            }
            return Ok(pharmacyMedicines.ToList());

        }
    }
}
