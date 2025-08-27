using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/")] // <-- adds prefix "api/lookups"
    public class LookupsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LookupsController(AppDbContext db)
        {
            _db = db;
        }

        // GET api/lookups/patients
        [HttpGet("patients")]
        public async Task<IActionResult> Patients()
            => Ok(await _db.Patients.OrderBy(x => x.Name).ToListAsync());

        // GET api/lookups/doctors
        [HttpGet("doctors")]
        public async Task<IActionResult> Doctors()
            => Ok(await _db.Doctors.OrderBy(x => x.Name).ToListAsync());

        // GET api/lookups/medicines
        [HttpGet("medicines")]
        public async Task<IActionResult> Medicines()
            => Ok(await _db.Medicines.OrderBy(x => x.Name).ToListAsync());
    }
}
