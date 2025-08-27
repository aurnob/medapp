using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class PrescriptionDetailRepository : IPrescriptionDetailRepository
    {
        private readonly AppDbContext _context;
        public PrescriptionDetailRepository(AppDbContext context) => _context = context;

        // Get all prescriptions for a specific appointment
        public async Task<IEnumerable<PrescriptionDetail>> GetAllAsync() =>
            await _context.PrescriptionDetails // optional: eager load Medicine details
                          .ToListAsync();

        // Get prescription detail by its ID
        public async Task<PrescriptionDetail?> GetByIdAsync(int id) =>
            await _context.PrescriptionDetails
                          .Include(p => p.Medicine) // optional eager load
                          .FirstOrDefaultAsync(p => p.Id == id);

        // Add a new prescription detail
        public async Task AddAsync(PrescriptionDetail detail)
        {
            _context.PrescriptionDetails.Add(detail);
            await _context.SaveChangesAsync();
        }

        // Update an existing prescription detail
        public async Task UpdateAsync(PrescriptionDetail detail)
        {
            _context.PrescriptionDetails.Update(detail);
            await _context.SaveChangesAsync();
        }

        // Delete a prescription detail
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.PrescriptionDetails.FindAsync(id);
            if (entity != null)
            {
                _context.PrescriptionDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
