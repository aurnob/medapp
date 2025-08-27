using Api.Dtos;
using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentRepository repo) => _repo = repo;

        public async Task<PagedResult<AppointmentListItemDto>> GetAppointmentsAsync(
        int pageNumber,
        int pageSize,
        int? doctorId = null,
        string? visitType = null,
        string? search = null)
        {
            // Directly fetch the paged result of AppointmentListItemDto from the repository
            var pagedResult = await _repo.GetAppointmentsPagedAsync(pageNumber, pageSize, doctorId, visitType, search);

            // No further mapping needed if repository already returns AppointmentListItemDto
            return new PagedResult<AppointmentListItemDto>
            {
                Items = pagedResult.Items,
                TotalCount = pagedResult.TotalCount
            };
        }



        ////public async Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync()
        ////{
        ////    var list = await _repo.GetAppointmentsAsync();
        ////    return list.Select(a => new AppointmentDto
        ////    {
        ////        Id = a.Id,
        ////        PatientId = a.PatientId,
        ////        DoctorId = a.DoctorId,
        ////        AppointmentDate = a.AppointmentDate,
        ////        VisitType = a.VisitType,
        ////        Notes = a.Notes,
        ////        Diagnosis = a.Diagnosis,
        ////        CreatedAt = a.CreatedAt,
        ////        UpdatedAt = a.UpdatedAt
        ////    });
        ////}

        public async Task<AppointmentDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return new AppointmentDto
            {
                Id = entity.Id,
                PatientId = entity.PatientId,
                DoctorId = entity.DoctorId,
                AppointmentDate = entity.AppointmentDate,
                VisitType = entity.VisitType,
                Notes = entity.Notes,
                Diagnosis = entity.Diagnosis,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task AddAsync(AppointmentDto dto) =>
            await _repo.AddAsync(new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                VisitType = dto.VisitType,
                Notes = dto.Notes,
                Diagnosis = dto.Diagnosis,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            });

        public async Task UpdateAsync(AppointmentDto dto) =>
            await _repo.UpdateAsync(new Appointment
            {
                Id = dto.Id,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                VisitType = dto.VisitType,
                Notes = dto.Notes,
                Diagnosis = dto.Diagnosis,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            });

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
