using EHospital.Appointments.Model;
using Microsoft.EntityFrameworkCore;

namespace EHospital.Appointments.Data
{
    /// <summary>
    /// Database context for Appointments.
    /// </summary>
    public partial class EHospitalContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EHospitalContext"/> class.
        /// </summary>
        public EHospitalContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllergyDbContext(DbContextOptions{TContext})"/> class.
        /// </summary>
        /// <param name="options"></param>
        public EHospitalContext(DbContextOptions<EHospitalContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Get or Set Appointment's Bills Set.
        /// </summary>
        public virtual DbSet<AppointmentBill> AppointmentBills { get; set; }

        /// <summary>
        /// Get or Set Appointments Set.
        /// </summary>
        public virtual DbSet<Appointment> Appointments { get; set; }

        /// <summary>
        /// Autho generated method by EFCore for creating Entityes from DataBase
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentBill>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.InvoiceNumber)
                    .HasName("UC_InvoiceNumber")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("smallmoney");

                entity.Property(e => e.InvoiceNumber).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Appointment)
                    .WithOne(p => p.AppointmentBill)
                    .HasForeignKey<AppointmentBill>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentBills_Appointments");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.AppointmentDateTime).HasColumnType("datetime");

                entity.Property(e => e.Duration).HasDefaultValueSql("((15))");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('inspection')");
            });
        }
    }
}