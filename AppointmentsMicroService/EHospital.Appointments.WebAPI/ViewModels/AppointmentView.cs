using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Appointments.WebApi.ViewModels
{
    class AppointmentView
    {
        /// <summary>
        /// Appointmen's Id.
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// Purpose for Appointment.
        /// </summary>
        public string Purpose { get; set; }
    }
}
