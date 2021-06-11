using MedicineStockSupply.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineStockSupply.Services
{
    public class MedicineRepo : IMedicineRepo<MedicineStock>
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineSupplyRepo));
        static List<MedicineStock> medicines = new List<MedicineStock>()
        {
            new MedicineStock(){ MedicineName = "Dolo-650", ChemicalComposition = "Acetaminophen", TargetAilment = "General",
             DateOfExpiry = "2021/09/21", NumberOfTabletsInStock = 50},

            new MedicineStock(){ MedicineName = "Gaviscon", ChemicalComposition = "Aluminum, Oxide, Silicon, Sodium, " +
                "Carbonic Acid", TargetAilment = "General", DateOfExpiry = "2021/12/31", NumberOfTabletsInStock = 120},

            new MedicineStock(){ MedicineName = "Orthoherb", ChemicalComposition = "ErandaVasa, Vilvam, Nimba, " +
                "Dusparsha, Amalaki, Manjishta, Nirgundi, Chithrakam, Kataka, Gokshurah, Shatavari, Pashanabheda",
                TargetAilment = "Orthopaedics", DateOfExpiry = "2022/04/05", NumberOfTabletsInStock = 90},

            new MedicineStock(){ MedicineName = "Cholecalciferol", ChemicalComposition = "Carbonic Acid, Oxide, Vitamins",
                TargetAilment = "Orthopaedics", DateOfExpiry = "2022/01/05", NumberOfTabletsInStock = 150},

            new MedicineStock(){ MedicineName = "Cyclopam", ChemicalComposition = "Paracetamol, Dicyclomine",
                TargetAilment = "Gynaecology", DateOfExpiry = "2021/07/01", NumberOfTabletsInStock = 40},

            new MedicineStock(){ MedicineName = "Hilact", ChemicalComposition = "Vitamins, Calcium, Oxide",
                TargetAilment = "Gynaecology", DateOfExpiry = "2022/05/31", NumberOfTabletsInStock = 340}
        };
        public List<MedicineStock> GetAllMedicines()
        {
            return medicines;
        }

        public MedicineStock GetMedicineDetails(string med)
        {
            return medicines.Where(m => string.Equals(m.MedicineName, med, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}
