using AccessDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDB
{
    public class HabitacionRepository: IHabitacionRepository
    {
        private readonly DBContext _context;
        public HabitacionRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<Habitacion>> ObtenerTodosAsync()
        {
            return await _context.Habitacion.OrderByDescending(h => h.Id).ToListAsync();
        }

        public async Task<Habitacion> GetHabitacionByNumeroAsync(int numeroHabitacion)
        {
            return await _context.Habitacion.FirstOrDefaultAsync(c => c.NumeroHabitacion == numeroHabitacion);
        }

        public async Task<Habitacion> GetHabitacionByIdAsync(int id)
        {
            return await _context.Habitacion.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Habitacion> CreateAsync(Habitacion habitacion)
        {
            await _context.Habitacion.AddAsync(habitacion);
            await _context.SaveChangesAsync();
            return habitacion;
        }

        public async Task UpdateAsync(Habitacion habitacion)
        {
           _context.Habitacion.Update(habitacion);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var habitacion = await _context.Habitacion.FirstOrDefaultAsync(c => c.Id == id);
            _context.Habitacion.Remove(habitacion);
            await _context.SaveChangesAsync();
        }
        public async Task<int> totalHabitacionesAsync()
        {
            return await _context.Habitacion.CountAsync(); ;
        }
      


    }
}
