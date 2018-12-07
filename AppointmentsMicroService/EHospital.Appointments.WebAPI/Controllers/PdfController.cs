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
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace EHospital.Appointments.WebApi.Controllers
{
    [Route("api/PDF")]
    [ApiController]
    public class PdfController : ControllerBase
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
        public PdfController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet("AllAppointment")]
        public async Task<IActionResult> AllAppointment()
        {
            var appointments = _service.GetAllAppointments().Result;
            if (appointments.Count() == 0)
            {
                log.Warn("No Appointments Found");
                return NotFound("No Appointments Found");
            }


            return new ViewAsPdf("AllAppointment", Mapper.Map<IEnumerable<AppointmentView>>(appointments))
            {
                PageSize = Size.A4,
                FileName = "PDF Doc.pdf"
            };
        }

        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentForCreate _appointment)
        {
            var appointment = _service.CreateAppointment(Mapper.Map<Appointment>(_appointment));
            if (appointment == null)
            {
                log.Warn("No Appointments Found");
                return NotFound("No Appointments Found");
            }


            return new ViewAsPdf("Create", _appointment)
            {
                PageSize = Size.A4,
                FileName = "PDF Doc.pdf"
            };
        }
    }
}