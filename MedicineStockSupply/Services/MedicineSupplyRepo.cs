using MedicineStockSupply.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace MedicineStockSupply.Services
{
    public class MedicineSupplyRepo:IMedicineSupply
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineSupplyRepo));
        List<string> Pharmacies = new List<string>() 
        { "Max Pharmacy", "Appolo Pharmacy", "Synergy Pharmacy", "GoodWill Pharmacy" };
        List<PharmacyMedicineSupply> pharmacyMedicines = new List<PharmacyMedicineSupply>();

        public async Task<List<PharmacyMedicineSupply>> GetSupplies(string medicine, int demandCount)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var response = await client.GetAsync("http://localhost:40486/api/medicinestock/"+medicine);
                    //var medicines = await response.Content.ReadAsAsync<MedicineStock>(new[] { new JsonMediaTypeFormatter() });
                    var result = response.Content.ReadAsStringAsync().Result;
                    var medicines = JsonConvert.DeserializeObject<MedicineStock>(result);

                    //var m = medicines.MedicineName;
                    if (medicines.NumberOfTabletsInStock >= demandCount)
                        pharmacyMedicines.Add(new PharmacyMedicineSupply()
                        {
                            MedicineName = medicine,
                            PharmacyName = Pharmacies[0],
                            SupplyCount = demandCount
                        });
                }
            }
            catch(Exception e)
            {
                //logging
                _log4net.Error(e.Message);
                return pharmacyMedicines.ToList();
            }
            return pharmacyMedicines.ToList();
        }
    }
}
