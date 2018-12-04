using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHospital.Appointments.BusinessLogic.Contracts;
using EHospital.Appointments.Model;
using EHospital.Appointments.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHospital.Appointments.WebApi.Controllers
{
    [Route("api/appointmentbills")]
    [ApiController]
    public class AppointmentBillsController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager
                                                          .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            var appointmentBill = await _service.GetAppointmentBillById(id);
            log.Info("Getting Appointment's Bill by Id");
            if (appointmentBill == null)
            {
                log.Warn("Failed to get Bill");
                return NotFound("No AppointmentBill with such Id");
            }
            return Ok(Mapper.Map<BillView>(appointmentBill));
        }

        /// <summary>
        /// Get all Bills.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBills()
        {
            var appointmentBills = await _service.GetAllAppointmentBills();
            log.Info("Getting all bills");
            if (appointmentBills == null)
            {
                log.Warn("Failed to get bills");
                return NotFound("No AppointmentBill with such Id");
            }
            return Ok(Mapper.Map<IEnumerable<BillView>>(appointmentBills));
        }

        /// <summary>
        /// Create new AppointmentBill
        /// </summary>
        [HttpPost]
        public IActionResult CreateBill([FromBody] BillForCreate appointmentBill)
        {
            var appointmentBillToCreate = _service.CreateAppointmentBill(Mapper.Map<AppointmentBill>(appointmentBill));
            log.Info("Creating new Bill");
            if (!ModelState.IsValid)
            {
                log.Warn("Failed to create new bill");
                return BadRequest("Wrong AppointmentBill Input");
            }
            return Ok(Mapper.Map<BillView>(appointmentBillToCreate));
        }

        /// <summary>
        /// Update AppointmentBill by Id.
        /// </summary>
        /// <param name="id">AppointmentBill Id.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateBill(int id, [FromBody] BillForUpdate appointmentBill)
        {
            log.Info("Updating Bill");
            if (!ModelState.IsValid)
            {
                log.Warn("Failed To update Bill");
                return BadRequest("Wrong AppointmentBill Input");
            }
            return Ok(Mapper.Map<BillView>(_service.UpdateAppoitmentBill(id, Mapper.Map<AppointmentBill>(appointmentBill))));
        }

        /// <summary>
        /// Delete AppointmentBill By Id.
        /// </summary>
        /// <param name="id">ID.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteBill(int id)
        {
            log.Info("Deleting Bill");
            try
            {
                return Ok(Mapper.Map<BillView>(_service.DeleteAppoitmentBill(id)));
            }
            catch (NullReferenceException)
            {
                log.Warn("Failed to delete Bill");
                return NotFound("No Appointment with such Id");
            }
        }

        /// <summary>
        /// Get all Patient Appointments.
        /// </summary>
        /// <returns>Result of Http request.</returns>
        [HttpGet("pattient/{id}")]
        public async Task<IActionResult> GetAllPattientAppointmentBills(int id)
        {
            var appointmentBills = await _service.GetAllPatientAppointmentBills(id);
            log.Info("Getting all biils of client");
            if (appointmentBills == null)
            {
                log.Warn("Failed To get pattient's bills");
                return NotFound("No Patient Bills with such Id");
            }
            return Ok(Mapper.Map<IEnumerable<BillView>>(appointmentBills));
        }
    }
}