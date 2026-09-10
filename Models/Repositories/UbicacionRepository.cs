using AccessDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDB.Repositories
{
    public class UbicacionRepository : IUbicacionRepository
    {
        private readonly DBContext _context;

        public UbicacionRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Provincia>> GetProvinciasAsync()
        {
            return await _context.Provincia.AsNoTracking().OrderBy(p => p.Nombre).ToListAsync();
        }
        public async Task<Provincia> GetProvinciaByIdAsync(int id)
        {
            return await _context.Provincia.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Distrito>> GetDistritoByCantonAsync(int id)
        {
            return await _context.Distrito.AsNoTracking().Where(d => d.IdCanton == id).OrderBy(d => d.Nombre).ToListAsync();
        }

        public async Task<Distrito> GetDistritoByIdAsync(int id)
        {
            return await _context.Distrito.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Canton>> GetCantonByProvinciaAsync(int id)
        {
            return await _context.Canton.AsNoTracking().Where(c => c.IdProvincia == id).OrderBy(c => c.Nombre).ToListAsync();
        }

        public async Task<Canton> GetCantonByIdAsync(int id)
        {
            return await _context.Canton.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<string> GetCompleteUbicationAsync(int provinciaId, int distritoId, int cantonId)
        {
            var provincia = (await GetProvinciaByIdAsync(provinciaId))?.Nombre ?? "";
            var distrito = (await GetDistritoByIdAsync(distritoId))?.Nombre ?? "";
            var canton = (await GetCantonByIdAsync(cantonId))?.Nombre ?? "";

            return $"{provincia} - {canton} - {distrito}".Trim();
        }

    }
}
