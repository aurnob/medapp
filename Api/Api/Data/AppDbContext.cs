// Data/HospitalContext.cs
using Api.Dtos;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);

            modelBuilder.Entity<PrescriptionDetail>()
                .HasOne(p => p.Appointment)
                .WithMany(a => a.PrescriptionDetails)
                .HasForeignKey(p => p.AppointmentId);

            modelBuilder.Entity<PrescriptionDetail>()
                .HasOne(p => p.Medicine)
                .WithMany(m => m.PrescriptionDetails)
                .HasForeignKey(p => p.MedicineId);
        }
    }
}
