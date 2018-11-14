using System.Collections.Generic;
using DataAccessLayer.Entityes;

namespace BusinessLogicLayer.Services
{
    /// <summary>
    /// Abstrarion of Service for Appointments.
    /// </summary>
    public interface IAppointmentService
    {
        #region Appointment region
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

        #endregion

        #region AppointmentBiil
        /// <summary>
        /// Get all AppointmentBill's.
        /// </summary>
        /// <returns>Set of AppointmentBills.</returns>
        IEnumerable<AppointmentBill> GetAllAppointmentBills();

        /// <summary>
        /// Get AppointmentBill By Id.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <returns>Chosen Appointment.</returns>
        AppointmentBill GetAppointmentBillById(int id);

        /// <summary>
        /// Create new AppointmentBill.
        /// </summary>
        /// <param name="appointment"></param>
        AppointmentBill CreateAppointmentBill(AppointmentBill appointment);

        /// <summary>
        /// Delete AppointmentBill by Id.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        AppointmentBill DeleteAppoitmentBill(int id);

        /// <summary>
        /// Update AppointmentBill by Id.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <param name="appointment">new Appointment's Info.</param>
        AppointmentBill UpdateAppoitmentBill(int id, AppointmentBill appointment);
        #endregion

        /// <summary>
        /// Get all Appointments of the Patient.
        /// </summary>
        /// <param name="id">Patient Id.</param>
        /// <returns>Set of Appointments</returns>
        IEnumerable<Appointment> GetAllPatientAppointments(int id);

        /// <summary>
        /// Get all AppointmentBills of the Patient.
        /// </summary>
        /// <param name="id">Patient Id.</param>
        /// <returns>Set of AppointmentBills</returns>
        IEnumerable<AppointmentBill> GetAllPatientAppointmentBills(int id);
    }
}