using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entityes
{
    /// <summary>
    /// Appointment's Bill entity from DataBase
    /// </summary>
    public partial class AppointmentBill
    {
        /// <summary>
        /// Appointment's Id.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Unic InvoiceNumber
        /// </summary>
        public Guid InvoiceNumber { get; set; }

        /// <summary>
        /// Amount of money for Appointment.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Is Bill completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// SoftDelete Mechanism.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Appointment of this Bill.
        /// </summary>
        public Appointment Appointment { get; set; }
    }
}