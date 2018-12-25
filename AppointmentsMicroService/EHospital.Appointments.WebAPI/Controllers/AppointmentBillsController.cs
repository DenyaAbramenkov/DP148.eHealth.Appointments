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
    [Route("api/appointmentbills")]
    [ApiController]
    public class AppointmentBillsController : ControllerBase
    {
        private readonly IAuthDetailsProvider _authDetailsProvider;
        private readonly ILoggingProvider _loggingProvider;
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IAppointmentBillService _service;
        private readonly Shared.Configuration.IConfigurationProvider _configurationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentBillsController(IAppointmentBillService)"/> class.
        /// </summary>
        /// <param name="service">Service.</param>
        public AppointmentBillsController(IAppointmentBillService service, ILoggingProvider loggingProvider,
            IHttpClientWrapper httpClientWrapper, IAuthDetailsProvider authDetailsProvider, Shared.Configuration.IConfigurationProvider configurationProvider)
        {
            _service = service;
            _authDetailsProvider = authDetailsProvider;
            _httpClientWrapper = httpClientWrapper;
            _loggingProvider = loggingProvider;
            _configurationProvider = configurationProvider;
        }

        /// <summary>
        /// Get AppointmentBill by Id of Appointment.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <returns>Result of Http request.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBill(int id)
        {
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

            try
            {
                var appointmentBill = await _service.GetAppointmentBillById(id);
                if (appointmentBill == null)
                {
                    return NotFound("No AppointmentBill with such Id");
                }
                return Ok(Mapper.Map<BillView>(appointmentBill));
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
        /// Get all Bills.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBills()
        {
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

            try
            {
                var appointmentBills = await _service.GetAllAppointmentBills();
                if (appointmentBills == null)
                {

                    return NotFound("No AppointmentBill with such Id");
                }
                return Ok(Mapper.Map<IEnumerable<BillView>>(appointmentBills));
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
        /// Create new AppointmentBill
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateBill([FromBody] BillForCreate appointmentBill)
        {
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

            try
            {
                var appointmentBillToCreate = _service.CreateAppointmentBill(Mapper.Map<AppointmentBill>(appointmentBill));
                if (!ModelState.IsValid)
                {

                    return BadRequest("Wrong AppointmentBill Input");
                }
                return Ok(Mapper.Map<BillView>(appointmentBillToCreate));
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
        /// Update AppointmentBill by Id.
        /// </summary>
        /// <param name="id">AppointmentBill Id.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id, [FromBody] BillForUpdate appointmentBill)
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

                    return BadRequest("Wrong AppointmentBill Input");
                }
                return Ok(Mapper.Map<BillView>(_service.UpdateAppoitmentBill(id, Mapper.Map<AppointmentBill>(appointmentBill))));
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
        /// Delete AppointmentBill By Id.
        /// </summary>
        /// <param name="id">ID.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
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
                    return Ok(Mapper.Map<BillView>(_service.DeleteAppoitmentBill(id)));
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
        public async Task<IActionResult> GetAllPattientAppointmentBills(int id)
        {
            var authInfo = await _authDetailsProvider.GetUserAuthInfoAsync(_configurationProvider.BaseUrl, Request.Headers["Authorization"]);
            if (authInfo == null)
            {
                return Unauthorized();
            }

            try
            {
                var appointmentBills = await _service.GetAllPatientAppointmentBills(id);
                if (appointmentBills == null)
                {
                    return NotFound("No Patient Bills with such Id");
                }
                return Ok(Mapper.Map<IEnumerable<BillView>>(appointmentBills));
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