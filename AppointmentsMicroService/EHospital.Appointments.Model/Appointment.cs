using System;
using System.ComponentModel.DataAnnotations;

namespace EHospital.Appointments.Model
{
    /// <summary>
    /// Appointment entity from database.
    /// </summary>
    public class Appointment: IEntity
    {
        /// <summary>
        /// Appointmen's Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Pattient's Id.
        /// </summary>
        [Required]
        public int PatientId { get; set; }

        /// <summary>
        /// Doctor/nurse Id.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Data and Time of Appointment.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDateTime { get; set; }

        /// <summary>
        /// Duration of Appointment.
        /// </summary>
        [Required]
        public int Duration { get; set; }

        /// <summary>
        /// Purpose for Appointment.
        /// </summary>
        [EnumDataType(typeof(TypeOfPurpose))]
        public string Purpose { get; set; }

        /// <summary>
        /// SoftDeleted Mechanism.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Bill for this Appointment
        /// </summary>
        public AppointmentBill AppointmentBill { get; set; }
    }
}