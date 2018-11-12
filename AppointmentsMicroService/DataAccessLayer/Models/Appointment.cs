using System;

namespace DataAccessLayer.Models
{
    public class Appointment
    {
        int AppointmentId { get; set; }

        int PatientId { get; set; }

        int UserId { get; set; }

        DateTime AppointmentDataTime { get; set; }

        int Duration { get; set; }

        string Purpose { get; set; }

        bool IsDeleted { get; set; }
    }
}
