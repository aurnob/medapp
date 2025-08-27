using Api.Dtos;
using Api.Models;

namespace Api.Repositories
{
    public interface IAppointmentRepository
    {
        Task<PagedResult<AppointmentListItemDto>> GetAppointmentsPagedAsync(
            int pageNumber,
            int pageSize,
            int? doctorId = null,
            string? visitType = null,
            string? search = null
        );
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
        
    }
}
