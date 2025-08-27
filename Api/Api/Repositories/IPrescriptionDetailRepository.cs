using Api.Models;

namespace Api.Repositories
{
    public interface IPrescriptionDetailRepository
    {
        Task<IEnumerable<PrescriptionDetail>> GetAllAsync();
        Task<PrescriptionDetail?> GetByIdAsync(int id);
        Task AddAsync(PrescriptionDetail detail);
        Task UpdateAsync(PrescriptionDetail detail);
        Task DeleteAsync(int id);
    }
}
