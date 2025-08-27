using Api.Dtos;

namespace Api.Services
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<PrescriptionDetailDto>> GetAllAsync();
        Task<PrescriptionDetailDto?> GetByIdAsync(int id);
        Task AddAsync(PrescriptionDetailDto dto);
        Task UpdateAsync(PrescriptionDetailDto dto);
        Task DeleteAsync(int id);
    }
}
