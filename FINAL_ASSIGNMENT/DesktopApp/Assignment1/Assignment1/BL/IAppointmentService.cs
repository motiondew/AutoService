using assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.BL
{
    interface IAppointmentService
    {
        List<Appointment> Get();
        void Edit(Appointment appointment);
    }
}
