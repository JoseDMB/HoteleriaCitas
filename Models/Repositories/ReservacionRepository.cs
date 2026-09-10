using AccessDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDB.Repositories
{
    public class ReservacionRepository : IReservacionRepository
    {
        private readonly DBContext _context;

        public ReservacionRepository(DBContext context)
        {
            _context = context;
        }   

        public async Task<List<Reservacion>> ObtenerTodosAsync()
        {
            return await _context.Reservaciones.AsNoTracking().OrderByDescending(r => r.FechaIngreso).ToListAsync();
        }
        public async Task<Reservacion> ObtenerPorIdAsync(int id)
        {
            return await _context.Reservaciones.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Reservacion> ObtenerPorCodigoAsync(string codigo)
        {
            return await _context.Reservaciones.AsNoTracking().FirstOrDefaultAsync(r => r.CodigoReserva == codigo);
        }
        public async Task<Reservacion> CrearAsync(Reservacion reservacion)
        {
            await _context.Reservaciones.AddAsync(reservacion);
            await _context.SaveChangesAsync();
            return reservacion;
        }
        public async Task ActualizarAsync(Reservacion reservacion)
        {
            _context.Reservaciones.Update(reservacion);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarAsync(int id)
        {
            var reservacion = await ObtenerPorIdAsync(id);
                _context.Reservaciones.Remove(reservacion);
                await _context.SaveChangesAsync();
        }
        public async Task<int> ObtenerTotalAsync()
        {
            return await _context.Reservaciones.CountAsync();
        }
        public async Task<bool> HabitacionDisponibleAsync(int idHabitacion,DateTime inicio,DateTime fin,int? excludeReservacionId = null)
        {
            return !await _context.Reservaciones.AnyAsync(r =>
                r.IdHabitacion == idHabitacion &&
                (!excludeReservacionId.HasValue ||
                 r.Id != excludeReservacionId.Value) &&
                inicio < r.FechaSalida &&
                fin > r.FechaIngreso
            );
        }

        public async Task<bool> ExisteReservacionPorClienteAsync(int idCliente)
        {
            return await _context.Reservaciones.AnyAsync(r => r.IdCliente == idCliente);
        }
        public async Task<bool> ExisteReservacionPorHabitacionAsync(int idHabitacion)
        {
            return await _context.Reservaciones.AnyAsync(r => r.IdHabitacion == idHabitacion);
        }
    }
}