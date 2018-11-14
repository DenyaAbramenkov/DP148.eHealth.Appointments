using System;
using System.ComponentModel.DataAnnotations;
public enum MyEnum
{
    procedure,
    inspection
}
namespace DataAccessLayer.Entityes
{
    /// <summary>
    /// Appointment entity from database.
    /// </summary>
    public partial class Appointment
    {
        /// <summary>
        /// Appointmen's Id.
        /// </summary>
        public int AppointmentId { get; set; }

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
        [EnumDataType(typeof(MyEnum))]
        public string Purpose { get; set; }

        /// <summary>
        /// SoftDeleted Mechanism.
        /// </summary>
        public bool IsDeleted { get; set; }

        public AppointmentBill AppointmentBill { get; set; }
    }
}