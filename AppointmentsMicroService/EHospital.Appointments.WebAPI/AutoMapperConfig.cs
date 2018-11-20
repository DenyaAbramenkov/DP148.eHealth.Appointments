using AutoMapper;
using EHospital.Appointments.Model;
using EHospital.Appointments.WebApi.ViewModels;
using System.Linq;

namespace EHospital.Appointments.WebApi
{
    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperConfig"/> class.
        /// </summary>
        public AutoMapperConfig()
        {
            CreateMap<Appointment, AppointmentView>();
            CreateMap<AppointmentBill, BillView>();
            CreateMap<AppointmentForCreate, Appointment>();
            CreateMap<BillForCreate, AppointmentBill>();
        }
    }
}