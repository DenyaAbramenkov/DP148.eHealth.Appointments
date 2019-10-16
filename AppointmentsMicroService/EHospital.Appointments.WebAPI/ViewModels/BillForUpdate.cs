using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Appointments.WebApi.ViewModels
{
    public class BillForUpdate
    {
        /// <summary>
        /// Amount of money for Appointment.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Is Bill completed.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
