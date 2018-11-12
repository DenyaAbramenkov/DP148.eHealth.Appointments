using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class AppointmentContext: DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentBill> AppointmentBills { get; set; }

        public AppointmentContext(DbContextOptions contextOptions)
            : base(contextOptions)
        {
        }
    }
}
