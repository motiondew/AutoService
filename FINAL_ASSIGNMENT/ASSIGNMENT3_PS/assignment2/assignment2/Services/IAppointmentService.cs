using assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Services
{
    public interface IAppointmentService
    {
        bool Duplicate(Appointment appointment);
    }
}
