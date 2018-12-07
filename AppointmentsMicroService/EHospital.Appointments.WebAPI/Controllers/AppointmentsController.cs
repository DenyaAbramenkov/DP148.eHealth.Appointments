using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHospital.Appointments.BusinessLogic;
using EHospital.Appointments.BusinessLogic.Contracts;
using EHospital.Appointments.Model;
using EHospital.Appointments.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using SelectPdf;

namespace EHospital.Appointments.WebApi.Controllers
{
    /// <summary>
    /// Contoller for Appointments Query.
    /// </summary>
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager
                                                          .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Service for Appointments.
        /// </summary>
        private readonly IAppointmentService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsController(IAppointmentService)"/> class.
        /// </summary>
        /// <param name="service">Service.</param>
        public AppointmentsController(IAppointmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all Appointments
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAppointment()
        {
            var appointments = await _service.GetAllAppointments();
            log.Info("Getting All Appointments");
            if (appointments.Count() == 0)
            {
                log.Warn("No Appointments Found");
                return NotFound("No Appointments Found");
            }
            return new ViewAsPdf("AllAppointment", Mapper.Map<IEnumerable<AppointmentView>>(appointments));
            //return Ok(Mapper.Map<IEnumerable<AppointmentView>>(appointments));
        }
        
        /// <summary>
        /// Get Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <returns>Result of Http request.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _service.GetAppointmentById(id);
            log.Info("Getting AppointmentBy Id");
            if(appointment == null)
            {
                log.Warn("Failed to get appointment by Id");
                return NotFound("No Appointment with such Id");
            }
            return Ok(Mapper.Map<AppointmentView>(appointment));
        }

        /// <summary>
        /// Create new Appointment.
        /// </summary>
        [HttpPost]
        public IActionResult CreateAppointment([FromBody] AppointmentForCreate appointment)
        {
            var appointmentToCreate = _service.CreateAppointment(Mapper.Map<Appointment>(appointment));
            log.Info("Creating new Appointment");
            if (!ModelState.IsValid)
            {
                log.Warn("Failed to create");
                return BadRequest("Wrong AppointmentInput");
            }
            return Ok(Mapper.Map<AppointmentView>(appointmentToCreate));
        } 

        /// <summary>
        /// Update Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, [FromBody] AppointmentForCreate appointment)
        {
            log.Info("Updating Appointment");
            if (!ModelState.IsValid)
            {
                log.Warn("Failed to Update");
                return BadRequest("Wrong Appointment Input");
            }
            return Ok(Mapper.Map<AppointmentView>(_service.UpdateAppoitment(id, Mapper.Map<Appointment>(appointment))));
        }

        /// <summary>
        /// Delete Appointment's By Id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            log.Info("Deleting Appointment");
            try
            {
                return Ok(_service.DeleteAppoitment(id));
            }
            catch(NullReferenceException)
            {
                log.Warn("FailedToDelete");
                return NotFound("No Appointment with such Id");
            }
        }

        /// <summary>
        /// Get all Patient Appointments.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet("pattient/{id}")]
        public async Task<IActionResult> GetAllPattientAppointment(int id)
        {
            var appointments = await _service.GetAllPatientAppointments(id);
            log.Info("Getting all Pattiens Appointments");
            if (appointments == null)
            {
                log.Warn("No appointment was found");
                return NotFound("No Patient Appointment with such Id");
            }
            return Ok(Mapper.Map<IEnumerable<AppointmentView>>(appointments));
        }

        
    }


}
