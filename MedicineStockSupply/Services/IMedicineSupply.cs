using MedicineStockSupply.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineStockSupply.Services
{
    public interface IMedicineSupply
    {
        public List<string> GetAllPharmacies();
        public Task<List<PharmacyMedicineSupply>> GetSupplies(string medicine, int demandCount);
    }
}
