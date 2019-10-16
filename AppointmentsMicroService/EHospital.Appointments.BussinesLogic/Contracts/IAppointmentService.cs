using System.Collections.Generic;
using System.Threading.Tasks;
using EHospital.Appointments.Model;

namespace EHospital.Appointments.BusinessLogic.Contracts
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
        Task<IEnumerable<Appointment>> GetAllAppointments();

        /// <summary>
        /// Get Appointment By Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <returns>Chosen Appointment.</returns>
        Task<Appointment> GetAppointmentById(int id);

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

        /// <summary>
        /// Get all Appointments of the Patient.
        /// </summary>
        /// <param name="id">Patient Id.</param>
        /// <returns>Set of Appointments</returns>
        Task<IEnumerable<Appointment>> GetAllPatientAppointments(int id);
    }
}