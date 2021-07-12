using MedicineStockSupply.Model;
using MedicineStockSupply.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicineStockSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineStockController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineStockController));
        private IMedicineRepo<MedicineStock> _repo;

        public MedicineStockController(IMedicineRepo<MedicineStock> repo)
        {
            _repo = repo;
        }
        // GET: api/<MedicineStockController>
        [HttpGet]
        public IActionResult MedicineStockInformation()
        {
            if(_repo.GetAllMedicines() != null)
                return Ok(_repo.GetAllMedicines());
            return BadRequest("No Stock of Medicines");
        }

        // GET: api/<MedicineStockController>/<name>
        [HttpGet("{medicineName}")]
        public /*MedicineStock*/ IActionResult OneMedicineInformation(string medicineName)
        {
            try
            {
                var medicine = _repo.GetMedicineDetails(medicineName);
                return Ok(medicine);
            }
            catch
            {
                _log4net.Error("Medicine List is Empty");
                return BadRequest();
            }
        }
    }
}
