using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHospital.Appointments.BusinessLogic.Contracts;
using EHospital.Shared.Authorization;
using EHospital.Shared.Configuration;
using EHospital.Shared.HttpClientWrapper;
using EHospital.Shared.Logging;
using EHospital.Shared.Logging.Models;
using EHospital.Appointments.Model;
using EHospital.Appointments.WebApi.ViewModels;
using EHospital.Shared.HttpClientWrapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IAuthDetailsProvider _authDetailsProvider;
        private readonly ILoggingProvider _loggingProvider;
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IAppointmentService _service;
        private readonly Shared.Configuration.IConfigurationProvider _configurationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController(IAppointmentService)"/> class.
        /// </summary>
        /// <param name="service">Service.</param>
        public AppointmentsController(IAppointmentService service, ILoggingProvider loggingProvider,
            IHttpClientWrapper httpClientWrapper, IAuthDetailsProvider authDetailsProvider, Shared.Configuration.IConfigurationProvider configurationProvider)
        {
            _service = service;
            _authDetailsProvider = authDetailsProvider;
            _httpClientWrapper = httpClientWrapper;
            _loggingProvider = loggingProvider;
            _configurationProvider = configurationProvider;
        }
        
        /// <summary>
        /// Get all Appointments
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAppointment()
        {
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

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
                await _loggingProvider.LogErrorMessage(_configurationProvider.BaseUrl, new LoggingMessage
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return BadRequest("Some error was thrown:" + ex.Message);
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
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

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
                await _loggingProvider.LogErrorMessage(_configurationProvider.BaseUrl, new LoggingMessage
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return BadRequest("Some error was thrown:" + ex.Message);
            }
        }

        /// <summary>
        /// Create new Appointment.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentForCreate appointment)
        {
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

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
                await _loggingProvider.LogErrorMessage(_configurationProvider.BaseUrl, new LoggingMessage
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return BadRequest("Some error was thrown:" + ex.Message);
            }
        }

        /// <summary>
        /// Update Appointment by Id.
        /// </summary>
        /// <param name="id">Appointment's Id.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentForCreate appointment)
        {
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

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
                await _loggingProvider.LogErrorMessage(_configurationProvider.BaseUrl, new LoggingMessage
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return BadRequest("Some error was thrown:" + ex.Message);
            }

        }

        /// <summary>
        /// Delete Appointment's By Id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

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
                await _loggingProvider.LogErrorMessage(_configurationProvider.BaseUrl, new LoggingMessage
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return BadRequest("Some error was thrown:" + ex.Message);
            }
        }

        /// <summary>
        /// Get all Patient Appointments.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet("pattient/{id}")]
        public async Task<IActionResult> GetAllPattientAppointment(int id)
        {
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

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
                await _loggingProvider.LogErrorMessage(_configurationProvider.BaseUrl, new LoggingMessage
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                    RequestType = HttpContext.Request.Method,
                    RequestUri = HttpContext.Request.GetDisplayUrl()
                });

                return BadRequest("Some error was thrown:" + ex.Message);
            }
        }
    }
}