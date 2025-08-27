using System.Data;
using Api.Data;
using Api.Dtos;
using Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context) => _context = context;

        public async Task<PagedResult<AppointmentListItemDto>> GetAppointmentsPagedAsync(
        int pageNumber,
        int pageSize,
        int? doctorId = null,
        string? visitType = null,
        string? search = null)
        {
            var results = new List<AppointmentListItemDto>();
            int totalCount = 0;

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_GetAppointmentsPaged";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
            cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
            cmd.Parameters.Add(new SqlParameter("@DoctorId", doctorId ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@VisitType", visitType ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@Search", search ?? (object)DBNull.Value));

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new AppointmentListItemDto
                {
                    AppointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                    AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                    VisitType = reader.GetString(reader.GetOrdinal("VisitType")),
                    Notes = reader["Notes"] as string,
                    Diagnosis = reader["Diagnosis"] as string,
                    PatientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                    PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                    PatientEmail = reader.GetString(reader.GetOrdinal("PatientEmail")),
                    DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                    DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                    DoctorSpecialty = reader.GetString(reader.GetOrdinal("DoctorSpecialty"))
                });
            }

            // Move to second resultset
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                totalCount = reader.GetInt32(reader.GetOrdinal("TotalCount"));
            }

            return new PagedResult<AppointmentListItemDto>
            {
                Items = results,
                TotalCount = totalCount
            };
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync() =>
            await _context.Appointments.ToListAsync();

        public async Task<Appointment?> GetByIdAsync(int id) =>
            await _context.Appointments.FindAsync(id);

        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Appointments.FindAsync(id);
            if (entity != null)
            {
                _context.Appointments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
