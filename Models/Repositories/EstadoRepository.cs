using AccessDB.Models;
using Microsoft.EntityFrameworkCore;
namespace AccessDB
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly DBContext _context;
        public EstadoRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<EstadoReserva>> ObtenerTodasAsync()
        {
            return await _context.EstadoReserva.OrderByDescending(c => c.Id).ToListAsync();
        }
        public async Task<EstadoReserva> ObtenerPorIdAsync(int id)
        {
            return await _context.EstadoReserva.FindAsync(id);
        }
    }
}
