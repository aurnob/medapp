using Api.Dtos;
using Api.Models;

namespace Api.Services
{
    public interface IAppointmentService
    {
        Task<PagedResult<AppointmentListItemDto>> GetAppointmentsAsync(
        int pageNumber,
        int pageSize,
        int? doctorId = null,
        string? visitType = null,
        string? search = null);
        Task<AppointmentDto?> GetByIdAsync(int id);
        Task AddAsync(AppointmentDto dto);
        Task UpdateAsync(AppointmentDto dto);
        Task DeleteAsync(int id);
    }
}
