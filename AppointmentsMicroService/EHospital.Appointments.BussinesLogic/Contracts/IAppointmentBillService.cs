using EHospital.Appointments.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHospital.Appointments.BusinessLogic.Contracts
{
    public interface IAppointmentBillService
    {
        /// <summary>
        /// Get all AppointmentBill's.
        /// </summary>
        /// <returns>Set of AppointmentBills.</returns>
        Task<IEnumerable<AppointmentBill>> GetAllAppointmentBills();

        /// <summary>
        /// Get AppointmentBill By Id.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <returns>Chosen Appointment.</returns>
        Task<AppointmentBill> GetAppointmentBillById(int id);

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

        /// <summary>
        /// Get all AppointmentBills of the Patient.
        /// </summary>
        /// <param name="id">Patient Id.</param>
        /// <returns>Set of AppointmentBills</returns>
        Task<IEnumerable<AppointmentBill>> GetAllPatientAppointmentBills(int id);
    }
}
