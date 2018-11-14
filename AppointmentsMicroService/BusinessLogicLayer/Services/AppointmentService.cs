using System.Collections.Generic;
using DataAccessLayer.Repository;
using DataAccessLayer.Entityes;

namespace BusinessLogicLayer.Services
{
    /// <summary>
    /// Service for Appointments.
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        /// <summary>
        /// Repositories from DAL.
        /// </summary>
        readonly IGenericRepository<Appointment> _appointmentRepositiry;
        readonly IGenericRepository<AppointmentBill> _appointmentBillRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentService(IGenericRepository{T})"/> class.
        /// </summary>
        /// <param name="appointmenRepository">Appointmen's repository.</param>
        public AppointmentService(IGenericRepository<Appointment> appointmenRepository,
                                  IGenericRepository<AppointmentBill> appointmentBillRepository)
        {
            _appointmentRepositiry = appointmenRepository;
            _appointmentBillRepository = appointmentBillRepository;
        }

        #region Appointment
        /// <summary>
        /// Get all Appointment's.
        /// </summary>
        /// <returns>Set of Appointments.</returns>
        public IEnumerable<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in _appointmentRepositiry.GetAll())
            {
                if (appointment.IsDeleted == false)
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        /// <summary>
        /// Get Appointment By Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <returns>Chosen Appointment.</returns>
        public Appointment GetAppointmentById(int id)
        {
           Appointment appointment = _appointmentRepositiry.GetById(id);
           return appointment;
        }

        /// <summary>
        /// Create new Appointment.
        /// </summary>
        /// <param name="appointment"></param>
        public Appointment CreateAppointment(Appointment appointment)
        {
            _appointmentRepositiry.Create(appointment);
            return appointment;
        }

        /// <summary>
        /// Delete Appointmetn by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        public Appointment DeleteAppoitment(int id)
        {
            Appointment appointmentToDelete = _appointmentRepositiry.GetById(id);
            _appointmentRepositiry.GetById(id).IsDeleted = true;
            return appointmentToDelete;
        }

        /// <summary>
        /// Update Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <param name="appointment">new Appointment's Info.</param>
        public Appointment UpdateAppoitment(int id, Appointment appointment)
        {
            Appointment appointmentToUpdate = _appointmentRepositiry.GetById(id);
            appointmentToUpdate = _appointmentRepositiry.Update(appointment);
            return appointmentToUpdate;
        }
        #endregion

        #region AppointmentBill

        /// <summary>
        /// Get all AppointmentBills.
        /// </summary>
        /// <returns>Set of Appointments.</returns>
        public IEnumerable<AppointmentBill> GetAllAppointmentBills()
        {
            List<AppointmentBill> appointmentBills = new List<AppointmentBill>();
            foreach (AppointmentBill appointmentBill in _appointmentBillRepository.GetAll())
            {
                if (appointmentBill.IsDeleted == false)
                {
                    appointmentBills.Add(appointmentBill);
                }
            }
            return appointmentBills;
        }

        /// <summary>
        /// Get AppointmentBill By Id.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <returns>Chosen AppointmentBill.</returns>
        public AppointmentBill GetAppointmentBillById(int id)
        {
            AppointmentBill appointmentBill = _appointmentBillRepository.GetById(id);
            return appointmentBill;
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
            AppointmentBill appointmentBillToDelete = _appointmentBillRepository.GetById(id);
            appointmentBillToDelete.IsDeleted = true;
            return appointmentBillToDelete;
        }

        /// <summary>
        /// Update AppointmentBill by Id.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <param name="appointment">new Appointment's Info.</param>
        public AppointmentBill UpdateAppoitmentBill(int id, AppointmentBill appointmentBill)
        {
            AppointmentBill appointmentBillToUpdate = _appointmentBillRepository.GetById(id);
            appointmentBillToUpdate = _appointmentBillRepository.Update(appointmentBill);
            return appointmentBillToUpdate;
        }

        #endregion

        /// <summary>
        /// Get all Appointments of the Patient.
        /// </summary>
        /// <param name="id">Patient Id.</param>
        /// <returns>Set of Appointments</returns>
        public IEnumerable<Appointment> GetAllPatientAppointments(int id)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in _appointmentRepositiry.GetAll())
            {
                if (appointment.IsDeleted == false && appointment.PatientId == id)
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        /// <summary>
        /// Get all AppointmentBills of the Patient.
        /// </summary>
        /// <param name="id">Patient Id.</param>
        /// <returns>Set of AppointmentBills</returns>
        public IEnumerable<AppointmentBill> GetAllPatientAppointmentBills(int id)
        {
            List<AppointmentBill> appointmentBills = new List<AppointmentBill>();
            foreach (AppointmentBill appointmentBill in _appointmentBillRepository.GetAll())
            {
                if (appointmentBill.IsDeleted == false &&
                    _appointmentRepositiry.GetById(appointmentBill.AppointmentId).PatientId == id)
                {
                    appointmentBills.Add(appointmentBill);
                }
            }
            return appointmentBills;
        }
    }
}