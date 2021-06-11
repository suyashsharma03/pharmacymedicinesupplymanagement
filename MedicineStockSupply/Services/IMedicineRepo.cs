using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineStockSupply.Services
{
    public interface IMedicineRepo<T>
    {
        List<T> GetAllMedicines();
        T GetMedicineDetails(string med);
    }
}
