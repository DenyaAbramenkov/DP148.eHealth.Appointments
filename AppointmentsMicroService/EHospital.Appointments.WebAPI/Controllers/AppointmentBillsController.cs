using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHospital.Appointments.BusinessLogic.Contracts;
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
        private const string LOG_URI_ERROR = "https://localhost:50935/api/Logging/error";
        private const string LOG_MESSAGE = "Unexpected internal error!";

        private readonly IHttpClientWrapper _httpClientWrapper;

        /// <summary>
        /// Service for Appointments.
        /// </summary>
        private readonly IAppointmentBillService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentBillsController(IAppointmentBillService)"/> class.
        /// </summary>
        /// <param name="service">Service.</param>
        public AppointmentBillsController(IAppointmentBillService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get AppointmentBill by Id of Appointment.
        /// </summary>
        /// <param name="id">AppointmentBill's Id.</param>
        /// <returns>Result of Http request.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBill(int id)
        {
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
        /// Get all Bills.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBills()
        {
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
        /// Create new AppointmentBill
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateBill([FromBody] BillForCreate appointmentBill)
        {
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
        /// Update AppointmentBill by Id.
        /// </summary>
        /// <param name="id">AppointmentBill Id.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id, [FromBody] BillForUpdate appointmentBill)
        {
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
        /// Delete AppointmentBill By Id.
        /// </summary>
        /// <param name="id">ID.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
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
        public async Task<IActionResult> GetAllPattientAppointmentBills(int id)
        {
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