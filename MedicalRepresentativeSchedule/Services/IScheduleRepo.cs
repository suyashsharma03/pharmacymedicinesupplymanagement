using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeSchedule.Services
{
    public interface IScheduleRepo<T>
    {
        List<T> GetSchedule(DateTime date);
        List<T> GetList();
    }
}
