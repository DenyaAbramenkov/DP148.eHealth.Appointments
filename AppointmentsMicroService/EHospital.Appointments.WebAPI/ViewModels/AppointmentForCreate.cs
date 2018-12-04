using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Appointments.WebApi.ViewModels
{
    public class AppointmentForCreate
    {
        /// <summary>
        /// Pattient's Id.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Doctor/nurse Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Data and Time of Appointment.
        /// </summary>
        public DateTime AppointmentDateTime { get; set; }

        /// <summary>
        /// Duration of Appointment.
        /// </summary>
        public int Duration { get; set; }


        public string Purpose { get; set; }
    }
}
