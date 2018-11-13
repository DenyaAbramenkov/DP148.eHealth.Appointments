using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entityes;

namespace AppointmentsAPI.Controllers
{
    /// <summary>
    /// Contoller for Appointments Query.
    /// </summary>
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
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
        public IActionResult Get()
        {
            var appointments = _service.GetAllAppointments();
            if (appointments.Count() == 0)
            {
                return NotFound("No Appointments Found");
            }
            return Ok(appointments);
        }

        /// <summary>
        /// Get Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <returns>Result of Http request.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var appointment = _service.GetAppointmentById(id);
            if(appointment == null)
            {
                return NotFound("No Appointment with such Id");
            }
            return Ok(_service.GetAppointmentById(id));
        }

        /// <summary>
        /// Create new Appointment.
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] Appointment appointment)
        {
            var appointmentToCreate = _service.CreateAppointment(appointment);
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong AppointmentInput");
            }
            return Ok(appointmentToCreate);
        } 

        /// <summary>
        /// Update Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong Appointment Input");
            }
            return Ok(_service.UpdateAppoitment(id, appointment));
        }

        /// <summary>
        /// Delete Appointment's By Id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_service.DeleteAppoitment(id));
            }
            catch(NullReferenceException)
            {
                return NotFound("No Appointment with such Id");
            }
        }
    }
}
