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
        /// Repository from DAL.
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
        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _appointmentRepositiry.GetAll();
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
            Appointment appointmentToUpdate = _appointmentRepositiry.GetById(id);
            appointmentToUpdate = _appointmentRepositiry.Update(appointment);
            return appointmentToUpdate;
        }
    }
}