using AccessDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDB.Repositories
{
    public class TipoHabRepository: ITipoHabRepository
    {
        private readonly DBContext _context;

        public TipoHabRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<TipoHabitacion>> ObtenerTodasAsync()
        {
            return await _context.TipoHabitacion.AsNoTracking().ToListAsync();
        }
        public async Task<TipoHabitacion> ObtenerPorIdAsync(int id)
        {
            return await _context.TipoHabitacion.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }



    }
}
