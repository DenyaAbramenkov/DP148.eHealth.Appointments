using System.Collections.Generic;
using DataAccessLayer.Entityes;

namespace BusinessLogicLayer.Services
{
    /// <summary>
    /// Abstrarion of Service for Appointments.
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// Get all Appointment's.
        /// </summary>
        /// <returns>Set of Appointments.</returns>
        IEnumerable<Appointment> GetAllAppointments();

        /// <summary>
        /// Get Appointment By Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <returns>Chosen Appointment.</returns>
        Appointment GetAppointmentById(int id);

        /// <summary>
        /// Create new Appointment.
        /// </summary>
        /// <param name="appointment"></param>
        Appointment CreateAppointment(Appointment appointment);

        /// <summary>
        /// Delete Appointmetn by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        Appointment DeleteAppoitment(int id);

        /// <summary>
        /// Update Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <param name="appointment">new Appointment's Info.</param>
        Appointment UpdateAppoitment(int id, Appointment appointment);
    }
}
