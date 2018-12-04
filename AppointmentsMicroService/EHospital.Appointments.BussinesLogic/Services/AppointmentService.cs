using EHospital.Appointments.BusinessLogic.Contracts;
using EHospital.Appointments.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHospital.Appointments.BusinessLogic.Services
{
    /// <summary>
    /// Service for Appointments.
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        /// <summary>
        /// Repositorie from DAL.
        /// </summary>
        readonly IGenericRepository<Appointment> _appointmentRepositiry;
   
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentService(IGenericRepository{T})"/> class.
        /// </summary>
        /// <param name="appointmenRepository">Appointmen's repository.</param>
        public AppointmentService(IGenericRepository<Appointment> appointmenRepository)
        {
            _appointmentRepositiry = appointmenRepository;
        }

        /// <summary>
        /// Get all Appointment's.
        /// </summary>
        /// <returns>Set of Appointments.</returns>
        public Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            var appointments = _appointmentRepositiry.GetAll();
            return appointments;
        }

        /// <summary>
        /// Get Appointment By Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <returns>Chosen Appointment.</returns>
        public Task<Appointment> GetAppointmentById(int id)
        {
           return _appointmentRepositiry.GetById(id);
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
            Appointment appointmentToDelete = _appointmentRepositiry.GetById(id).Result;
            appointmentToDelete.IsDeleted = true;
            _appointmentRepositiry.Delete(appointmentToDelete);
            return appointmentToDelete;
        }

        /// <summary>
        /// Update Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <param name="appointment">new Appointment's Info.</param>
        public Appointment UpdateAppoitment(int id, Appointment appointment)
        {
            Appointment appointmentToUpdate = _appointmentRepositiry.GetById(id).Result;
            appointmentToUpdate.PatientId = appointment.PatientId;
            appointmentToUpdate.UserId = appointment.UserId;
            appointmentToUpdate.AppointmentDateTime = appointment.AppointmentDateTime;
            appointmentToUpdate.Purpose = appointment.Purpose;
            appointmentToUpdate.Duration = appointment.Duration;
            _appointmentRepositiry.Update(appointmentToUpdate);
            _appointmentRepositiry.SaveChanges();
            return appointmentToUpdate;
        }

        /// <summary>
        /// Get all Appointments of the Patient.
        /// </summary>
        /// <param name="id">Patient Id.</param>
        /// <returns>Set of Appointments</returns>
        public async Task<IEnumerable<Appointment>> GetAllPatientAppointments(int id)
        {
            var appointments = new List<Appointment>();
            foreach (Appointment appointment in await _appointmentRepositiry.GetAll())
            {
                if (appointment.PatientId == id)
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }
    }
}