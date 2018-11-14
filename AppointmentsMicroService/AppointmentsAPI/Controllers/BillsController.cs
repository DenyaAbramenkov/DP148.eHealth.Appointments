using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Controllers
{
    [Route("api/bills")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        /// <summary>
        /// Service for Appointments.
        /// </summary>
        private readonly IAppointmentService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsController(IAppointmentService)"/> class.
        /// </summary>
        /// <param name="service">Service.</param>
        public BillsController(IAppointmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get AppointmentBill by Id of Appointment.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <returns>Result of Http request.</returns>
        [HttpGet("{id}")]
        public IActionResult GetBill(int id)
        {
            var appointment = _service.GetAppointmentBillById(id);
            if (appointment == null)
            {
                return NotFound("No AppointmentBill with such Id");
            }
            return Ok(appointment);
        }

        [HttpGet]
        public IActionResult GetAllBills()
        {
            var appointments = _service.GetAllAppointmentBills();
            if (appointments == null)
            {
                return NotFound("No AppointmentBill with such Id");
            }
            return Ok(appointments);
        }

    }
}