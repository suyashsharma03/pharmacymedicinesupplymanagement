using MedicalRepresentativeSchedule.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeSchedule.Services
{
    public class ScheduleRepo : IScheduleRepo<RepSchedule>
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ScheduleRepo));
        public ScheduleRepo()
        {

        }
        public List<RepSchedule> GetList()
        {
            List<RepSchedule> scheduleDetails = new List<RepSchedule>();
            _log4net.Info("Data is read from CSV");
            try
            {
                using (StreamReader sr = new StreamReader("doctordetails.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        scheduleDetails.Add(new RepSchedule() { Name = values[0], DoctorName = values[1], MeetingSlot = values[5], DateOfMeeting = Convert.ToDateTime(values[6]), DoctorContactNumber = Convert.ToInt64(values[7]) });
                    }

                }
            }
            catch (NullReferenceException e)
            {
                //logging
                _log4net.Error(e.Message);
                return null;
            }
            return scheduleDetails.ToList();
        }
        public List<RepSchedule> GetSchedule(DateTime date)
        {

            if (date.DayOfWeek != 0)
            {
                List<RepSchedule> scheduleDetails = new List<RepSchedule>();
                var count = GetList().Where(c => c.DateOfMeeting == date).Count();
                if (count <= 5)
                {
                    var list = GetList().Where(c => c.DateOfMeeting == date);
                    foreach(var l in list)
                        scheduleDetails.Add(l);
                }
                else
                {
                    _log4net.Error("Appointment for the given date is greater than 5");
                }
                return scheduleDetails; 
            }
            else
            {
                _log4net.Error("No Meetings Scheduled");
                return null;
            }
        }
    }
}
