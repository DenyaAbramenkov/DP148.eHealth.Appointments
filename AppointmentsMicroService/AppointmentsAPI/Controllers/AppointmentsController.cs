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
        public IActionResult GetAllAppointment()
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
        public IActionResult GetAppointment(int id)
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
        public IActionResult CreateAppointment([FromBody] Appointment appointment)
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
        public IActionResult UpdateAppointment(int id, [FromBody] Appointment appointment)
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
        public IActionResult DeleteAppointment(int id)
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

        /// <summary>
        /// Get AppointmentBill by Id of Appointment.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <returns>Result of Http request.</returns>
        [HttpGet("{id}/bills")]
        public IActionResult GetBill(int id)
        {
            var appointment = _service.GetAppointmentBillById(id);
            if (appointment == null)
            {
                return NotFound("No AppointmentBill with such Id");
            }
            return Ok(appointment);
        }

        /// <summary>
        /// Get all Bills.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet("/bills")]
        public IActionResult GetAllBills()
        {
            var appointments = _service.GetAllAppointmentBills();
            if (appointments == null)
            {
                return NotFound("No AppointmentBill with such Id");
            }
            return Ok(appointments);
        }

        /// <summary>
        /// Create new AppointmentBill
        /// </summary>
        [HttpPost("bills")]
        public IActionResult CreateBill([FromBody] AppointmentBill appointmentBill)
        {
            var appointmentToCreate = _service.CreateAppointmentBill(appointmentBill);
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong AppointmentBill Input");
            }
            return Ok(appointmentToCreate);
        }

        /// <summary>
        /// Update AppointmentBill by Id.
        /// </summary>
        /// <param name="id">AppointmentBill Id.</param>
        [HttpPut("{id}/bills")]
        public IActionResult UpdateBill(int id, [FromBody] AppointmentBill appointmentBill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong AppointmentBill Input");
            }
            return Ok(_service.UpdateAppoitmentBill(id, appointmentBill));
        }

        /// <summary>
        /// Delete AppointmentBill By Id.
        /// </summary>
        /// <param name="id">ID.</param>
        [HttpDelete("{id}/bills")]
        public IActionResult DeleteBill(int id)
        {
            try
            {
                return Ok(_service.DeleteAppoitmentBill(id));
            }
            catch (NullReferenceException)
            {
                return NotFound("No Appointment with such Id");
            }
        }

        /// <summary>
        /// Get all Patient Appointments.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet("pattient/{id}")]
        public IActionResult GetAllPattientAppointment(int id)
        {
            var appointments = _service.GetAllPatientAppointments(id);
            if (appointments == null)
            {
                return NotFound("No Patient Appointment with such Id");
            }
            return Ok(appointments);
        }

        /// <summary>
        /// Get all Patient Appointments.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet("bills/pattient/{id}")]
        public IActionResult GetAllPattientAppointmentBills(int id)
        {
            var appointmentBills = _service.GetAllPatientAppointmentBills(id);
            if (appointmentBills == null)
            {
                return NotFound("No Patient Bills with such Id");
            }
            return Ok(appointmentBills);
        }

    }
}
