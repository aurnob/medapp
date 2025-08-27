using Api.Dtos;
using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class PrescriptionService
    {
        
        private readonly IPrescriptionDetailRepository _repo;

        public PrescriptionService(IPrescriptionDetailRepository repo) => _repo = repo;

        public async Task<IEnumerable<PrescriptionDetailDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(p => new PrescriptionDetailDto
            {
                Id = p.Id,
                AppointmentId = p.AppointmentId,
                MedicineId = p.MedicineId,
                Dosage = p.Dosage,
                Instructions = p.Instructions,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });
        }

        public async Task<PrescriptionDetailDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            return new PrescriptionDetailDto
            {
                Id = entity.Id,
                AppointmentId = entity.AppointmentId,
                MedicineId = entity.MedicineId,
                Dosage = entity.Dosage,
                Instructions = entity.Instructions,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task AddAsync(PrescriptionDetailDto dto) =>
            await _repo.AddAsync(new PrescriptionDetail
            {
                AppointmentId = dto.AppointmentId,
                MedicineId = dto.MedicineId,
                Dosage = dto.Dosage,
                Instructions = dto.Instructions,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            });

        public async Task UpdateAsync(PrescriptionDetailDto dto) =>
            await _repo.UpdateAsync(new PrescriptionDetail
            {
                Id = dto.Id,
                AppointmentId = dto.AppointmentId,
                MedicineId = dto.MedicineId,
                Dosage = dto.Dosage,
                Instructions = dto.Instructions,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            });

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
