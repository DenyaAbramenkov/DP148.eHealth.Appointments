using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHospital.Appointments.Model
{
    /// <summary>
    /// Appointment's Bill entity from DataBase
    /// </summary>
    public class AppointmentBill: IEntity
    {
        /// <summary>
        /// Appointment's Id.
        /// </summary>
        public int Id { get; set; }

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
        public virtual Appointment Appointment { get; set; }
    }
}