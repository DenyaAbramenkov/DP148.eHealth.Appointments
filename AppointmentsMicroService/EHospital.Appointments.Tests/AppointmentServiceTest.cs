using System.Collections.Generic;
using Abp.Domain.Uow;
using EHospital.Appointments.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using  EHospital.Appointments.Model;

namespace EHospital.Appointments.Tests
{
    [TestClass]
    public class AppointmentServiceTest
    {
        Mock<IGenericRepository<Appointment>> _mockAppointment;
        Mock<EHospitalContext> _mockContext;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAppointment = new Mock<IGenericRepository<Appointment>>();
            _mockContext = new Mock<EHospitalContext>();
            _mockContext.Setup(s => s.Appointments);
        }

        [TestMethod]
        public void AppointmentGetById()
        {
            // more code here (new up the service, then test the service method, etc)
        }
    }
}
