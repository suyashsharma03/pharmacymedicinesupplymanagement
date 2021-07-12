using MedicalRepresentativeSchedule.Model;
using MedicalRepresentativeSchedule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MedicalRepresentativeSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepScheduleController : ControllerBase
    {
        private IScheduleRepo<RepSchedule> _scheduleRepo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RepScheduleController));
        public RepScheduleController(IScheduleRepo<RepSchedule> scheduleRepo)
        {
            _scheduleRepo = scheduleRepo;
        }
        [HttpGet("{scheduleStartDate}")]
        public IActionResult GetSchedule(string scheduleStartDate)
        {
            List<RepSchedule> scheduleList = new List<RepSchedule>();
            try
            {
                DateTime startDate = DateTime.Parse(scheduleStartDate);
                scheduleList = _scheduleRepo.GetSchedule(startDate);
                if (scheduleList == null)
                {
                    _log4net.Error("List is Null");
                    throw new InvalidOperationException("Nothing to Display");
                }
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception)
            {
                _log4net.Error("Couldn't Convert Date Properly");
                return BadRequest();
            }
            return Ok(scheduleList);
        }
    }
}
