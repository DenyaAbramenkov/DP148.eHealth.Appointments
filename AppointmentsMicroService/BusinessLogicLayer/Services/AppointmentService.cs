using System;
using System.Collections.Generic;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLogicLayer.Services
{
    public class AppointmentService : IAppointmentService
    {
        readonly IGenericRepository<Appointment> _appointmentRepositiry;

        public AppointmentService(IGenericRepository<Appointment> db)
        {
            _appointmentRepositiry = db;
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            IEnumerable<Appointment> appointments = _appointmentRepositiry.GetAll();
            return appointments;
        }

        public Appointment GetAppointmentById(int id)
        {
            return new Appointment();
        }

        public void CreateAppointment(Appointment appointment)
        {
            
        }

        public void DeleteAppoitment(int id)
        {
           
        }

        public void UpdateAppoitment(int id, Appointment appointment)
        {
           
        }
    }
}
