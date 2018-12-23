using System.Collections.Generic;
using System.Threading.Tasks;
using EHospital.Appointments.BusinessLogic.Contracts;
using EHospital.Appointments.Model;
using JetBrains.Annotations;

namespace EHospital.Appointments.BusinessLogic.Services
{
    /// <summary>
    ///     Service for Appointments.
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        /// <summary>
        ///     Repository from DAL.
        /// </summary>
        private readonly IGenericRepository<Appointment> _appointmentRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AppointmentService" /> class.
        /// </summary>
        /// <param name="appointmentRepository">Appointment's repository.</param>
        public AppointmentService(IGenericRepository<Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        /// <summary>
        ///     Get all Appointment's.
        /// </summary>
        /// <returns>Set of Appointments.</returns>
        public Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            var appointments = _appointmentRepository.GetAll();
            return appointments;
        }

        /// <summary>
        ///     Get Appointment By Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <returns>Chosen Appointment.</returns>
        public Task<Appointment> GetAppointmentById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        /// <summary>
        ///     Create new Appointment.
        /// </summary>
        /// <param name="appointment"></param>
        public Appointment CreateAppointment(Appointment appointment)
        {
            _appointmentRepository.Create(appointment);
            return appointment;
        }

        /// <summary>
        ///     Delete Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        public Appointment DeleteAppointment(int id)
        {
            var appointmentToDelete = _appointmentRepository.GetById(id).Result;
            appointmentToDelete.IsDeleted = true;
            _appointmentRepository.Delete(appointmentToDelete);
            return appointmentToDelete;
        }

        /// <summary>
        ///     Update Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <param name="appointment">new Appointment's Info.</param>
        public Appointment UpdateAppointment(int id, Appointment appointment)
        {
            var appointmentToUpdate = _appointmentRepository.GetById(id).Result;
            appointmentToUpdate.PatientId = appointment.PatientId;
            appointmentToUpdate.UserId = appointment.UserId;
            appointmentToUpdate.AppointmentDateTime = appointment.AppointmentDateTime;
            appointmentToUpdate.Purpose = appointment.Purpose;
            appointmentToUpdate.Duration = appointment.Duration;
            _appointmentRepository.Update(appointmentToUpdate);
            _appointmentRepository.SaveChanges();
            return appointmentToUpdate;
        }

        /// <summary>
        ///     Get all Appointments of the Patient.
        /// </summary>
        /// <param name="id">Patient Id.</param>
        /// <returns>Set of Appointments</returns>
        public async Task<IEnumerable<Appointment>> GetAllPatientAppointments(int id)
        {
            var appointments = new List<Appointment>();
            foreach (var appointment in await _appointmentRepository.GetAll())
                if (appointment.PatientId == id)
                    appointments.Add(appointment);
            return appointments;
        }
    }
}