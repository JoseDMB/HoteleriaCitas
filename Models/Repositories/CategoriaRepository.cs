using AccessDB.Models;
using Microsoft.EntityFrameworkCore;
namespace AccessDB
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DBContext _context;
        public CategoriaRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<Categoria>> ObtenerTodasAsync()
        {
            return await _context.Categoria.OrderByDescending(c => c.Id).ToListAsync();
        }
        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            return await _context.Categoria.FindAsync(id);
        }
    }
}
