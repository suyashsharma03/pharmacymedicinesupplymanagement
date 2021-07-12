using PharmacyMedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyMedicineSupplyPortal.Repository
{
    public class SuppliesRepository : ISupplies
    {
        private EFDbContext context;
        public SuppliesRepository(EFDbContext context)
        {
            this.context = context;
        }
        public int AddSupply(Supplies supply)
        {
            context.Supplies.Add(supply);
            context.SaveChanges();
            return supply.SupplyId;
        }

        /*public void DeleteSupply(int id)
        {
            try
            {
                var supply = context.Supplies.Where(s => s.SupplyId == id).FirstOrDefault();
                context.Remove(supply);
            }
            catch
            {
                //logging
            }

        }*/
    }
}
