using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHospital.Appointments.BusinessLogic;
using EHospital.Appointments.BusinessLogic.Contracts;
using EHospital.Appointments.Model;
using EHospital.Appointments.WebApi.ViewModels;
using EHospital.Shared.HttpClientWrapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EHospital.Appointments.WebApi.Controllers
{
    /// <summary>
    /// Contoller for Appointments Query.
    /// </summary>
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private const string LOG_URI_ERROR = "https://localhost:50935/api/Logging/error";
        private const string LOG_MESSAGE = "Unexpected internal error!";

        private readonly IHttpClientWrapper _httpClientWrapper;

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
            try
            {
                var appointments = await _service.GetAllAppointments();
                if (appointments.Count() == 0)
                {
                    return NotFound("No Appointments Found");
                }
                return Ok(Mapper.Map<IEnumerable<AppointmentView>>(appointments));
            }

            catch (Exception ex)
            {
                await _httpClientWrapper.SendPostRequest(LOG_URI_ERROR, new
                {
                    Message = LOG_MESSAGE,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return Conflict();
            }
        }

        /// <summary>
        /// Get Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        /// <returns>Result of Http request.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            try
            {
                var appointment = await _service.GetAppointmentById(id);
                if (appointment == null)
                {
                    return NotFound("No Appointment with such Id");
                }
                return Ok(Mapper.Map<AppointmentView>(appointment));
            }

            catch (Exception ex)
            {
                await _httpClientWrapper.SendPostRequest(LOG_URI_ERROR, new
                {
                    Message = LOG_MESSAGE,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return Conflict();
            }
        }

        /// <summary>
        /// Create new Appointment.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentForCreate appointment)
        {
            try
            {
                var appointmentToCreate = _service.CreateAppointment(Mapper.Map<Appointment>(appointment));
                if (!ModelState.IsValid)
                {
                    return BadRequest("Wrong AppointmentInput");
                }
                return Ok(Mapper.Map<AppointmentView>(appointmentToCreate));
            }

            catch (Exception ex)
            {
                await _httpClientWrapper.SendPostRequest(LOG_URI_ERROR, new
                {
                    Message = LOG_MESSAGE,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return Conflict();
            }
        }

        /// <summary>
        /// Update Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentForCreate appointment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Wrong Appointment Input");
                }
                return Ok(Mapper.Map<AppointmentView>(_service.UpdateAppointment(id, Mapper.Map<Appointment>(appointment))));
            }

            catch (Exception ex)
            {
                await _httpClientWrapper.SendPostRequest(LOG_URI_ERROR, new
                {
                    Message = LOG_MESSAGE,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return Conflict();
            }

        }

        /// <summary>
        /// Delete Appointment's By Id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                try
                {
                    return Ok(_service.DeleteAppointment(id));
                }
                catch (NullReferenceException)
                {
                    return NotFound("No Appointment with such Id");
                }
            }

            catch (Exception ex)
            {
                await _httpClientWrapper.SendPostRequest(LOG_URI_ERROR, new
                {
                    Message = LOG_MESSAGE,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return Conflict();
            }
        }

        /// <summary>
        /// Get all Patient Appointments.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet("pattient/{id}")]
        public async Task<IActionResult> GetAllPattientAppointment(int id)
        {
            try
            { 
            var appointments = await _service.GetAllPatientAppointments(id);
            if (appointments == null)
            {
                return NotFound("No Patient Appointment with such Id");
            }
            return Ok(Mapper.Map<IEnumerable<AppointmentView>>(appointments));
            }
            catch (Exception ex)
            {
                await _httpClientWrapper.SendPostRequest(LOG_URI_ERROR, new
                {
                    Message = LOG_MESSAGE,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return Conflict();
            }
        }
    }
}