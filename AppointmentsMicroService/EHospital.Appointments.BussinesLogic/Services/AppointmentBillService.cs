using EHospital.Appointments.BusinessLogic.Contracts;
using EHospital.Appointments.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHospital.Appointments.BusinessLogic.Services
{
    public class AppointmentBillService:IAppointmentBillService
    {
        /// <summary>
        /// Repositorie from DAL.
        /// </summary>
        readonly IGenericRepository<AppointmentBill> _appointmentBillRepository;
        readonly IGenericRepository<Appointment> _appointmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentBillService(IGenericRepository{T})"/> class.
        /// </summary>
        /// <param name="appointmenRepository">Appointmen's repository.</param>
        public AppointmentBillService(IGenericRepository<AppointmentBill> appointmentBillRepository,
                                      IGenericRepository<Appointment> appointmentRepository)
        {
            _appointmentBillRepository = appointmentBillRepository;
            _appointmentRepository = appointmentRepository;
        }

        /// <summary>
        /// Get all AppointmentBills.
        /// </summary>
        /// <returns>Set of Appointments.</returns>
        public Task<IEnumerable<AppointmentBill>> GetAllAppointmentBills()
        {
            var appointmentBills = _appointmentBillRepository.GetAll();
            return appointmentBills;
        }

        /// <summary>
        /// Get AppointmentBill By Id.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <returns>Chosen AppointmentBill.</returns>
        public Task<AppointmentBill> GetAppointmentBillById(int id)
        {
            return _appointmentBillRepository.GetById(id);
        }

        /// <summary>
        /// Create new AppointmentBill.
        /// </summary>
        /// <param name="appointmentBill"></param>
        public AppointmentBill CreateAppointmentBill(AppointmentBill appointmentBill)
        {
            _appointmentBillRepository.Create(appointmentBill);
            return appointmentBill;
        }

        /// <summary>
        /// Delete AppointmentBill by Id.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        public AppointmentBill DeleteAppoitmentBill(int id)
        {
            AppointmentBill appointmentBillToDelete = _appointmentBillRepository.GetById(id).Result;
            appointmentBillToDelete.IsDeleted = true;
            _appointmentBillRepository.Delete(appointmentBillToDelete);
            return appointmentBillToDelete;
        }

        /// <summary>
        /// Update AppointmentBill by Id.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <param name="appointment">new Appointment's Info.</param>
        public AppointmentBill UpdateAppoitmentBill(int id, AppointmentBill appointmentBill)
        {
            AppointmentBill appointmentBillToUpdate = _appointmentBillRepository.GetById(id).Result;
            appointmentBillToUpdate.Id = appointmentBill.Id;
            appointmentBillToUpdate.InvoiceNumber = appointmentBill.InvoiceNumber;
            appointmentBillToUpdate.InvoiceNumber = appointmentBill.InvoiceNumber;
            appointmentBillToUpdate.Amount = appointmentBill.Amount;
            appointmentBillToUpdate = _appointmentBillRepository.Update(appointmentBill);
            return appointmentBillToUpdate;
        }

        /// <summary>
        /// Get all AppointmentBills of the Patient.
        /// </summary>
        /// <param name="id">Patient Id.</param>
        /// <returns>Set of AppointmentBills</returns>
        public async Task<IEnumerable<AppointmentBill>> GetAllPatientAppointmentBills(int id)
        {
            var appointmentBills = new List<AppointmentBill>();
            foreach (AppointmentBill appointmentBill in await _appointmentBillRepository.GetAll())
            {
                if (_appointmentRepository.GetById(appointmentBill.Id).Result.PatientId == id)
                {
                    appointmentBills.Add(appointmentBill);
                }
            }
            return appointmentBills;
        }
    }
}
