using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAllAppointments();

        Appointment GetAppointmentById(int id);

        void CreateAppointment(Appointment appointment);

        void UpdateAppoitment(int id, Appointment appointment);

        void DeleteAppoitment(int id);
    }
}
