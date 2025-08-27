using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public static class Seed
    {
        public static async Task RunAsync(AppDbContext db)
        {
            await db.Database.MigrateAsync();
            var now = DateTime.UtcNow;

            // Seed Patients if none exist
            if (!await db.Patients.AnyAsync())
            {
                for (int i = 1; i <= 20; i++)
                {
                    db.Patients.Add(new Patient
                    {
                        Name = $"Patient {i}",
                        Email = $"patient{i}@example.com",
                        DateOfBirth = now.AddYears(-20).AddDays(i * 10),
                        CreatedAt = now
                    });
                }
                await db.SaveChangesAsync();
            }

            // Seed Doctors if none exist
            if (!await db.Doctors.AnyAsync())
            {
                string[] specialties = { "GP", "Cardiology", "Dermatology", "Neurology", "Pediatrics" };
                for (int i = 1; i <= 10; i++)
                {
                    db.Doctors.Add(new Doctor
                    {
                        Name = $"Dr. Doctor{i}",
                        Specialty = specialties[i % specialties.Length],
                        CreatedAt = now
                    });
                }
                await db.SaveChangesAsync();
            }

            // Seed Medicines if none exist (unchanged)
            if (!await db.Medicines.AnyAsync())
            {
                db.Medicines.AddRange(
                    new Medicine { Name = "Paracetamol", Manufacturer = "Acme Pharma", CreatedAt = now },
                    new Medicine { Name = "Metformin", Manufacturer = "HealthCorp", CreatedAt = now },
                    new Medicine { Name = "Amoxicillin", Manufacturer = "GenericLabs", CreatedAt = now }
                );
                await db.SaveChangesAsync();
            }

            // Seed Appointments if none exist
            if (!await db.Appointments.AnyAsync())
            {
                var patients = await db.Patients.ToListAsync();
                var doctors = await db.Doctors.ToListAsync();

                var rnd = new Random();

                for (int i = 0; i < 30; i++)
                {
                    var patient = patients[rnd.Next(patients.Count)];
                    var doctor = doctors[rnd.Next(doctors.Count)];

                    db.Appointments.Add(new Appointment
                    {
                        PatientId = patient.Id,
                        DoctorId = doctor.Id,
                        AppointmentDate = now.AddDays(rnd.Next(1, 60)), // random date within next 2 months
                        VisitType = rnd.Next(0, 2) == 0 ? "First" : "Follow-up", // Random visit type
                        Notes = "Sample notes " + i,
                        Diagnosis = "Sample diagnosis " + i,
                        CreatedAt = now
                    });
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
