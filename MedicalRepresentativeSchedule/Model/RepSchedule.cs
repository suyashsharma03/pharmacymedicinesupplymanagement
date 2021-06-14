using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeSchedule.Model
{
    public class RepSchedule
    {
        public string Name { get; set; }
        public string DoctorName { get; set; }
        public string TreatingAilment { get; set; }
        public string Medicine { get; set; }
        public string MeetingSlot { get; set; }
        public DateTime DateOfMeeting { get; set; }
        public long DoctorContactNumber { get; set; }
    }
}
