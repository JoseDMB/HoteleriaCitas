using AccessDB.Models;
namespace API_Hotel.Interfaces
{
    public interface IReservacionService
    {
        Task<List<Reservacion>> ObtenerTodosAsync();
        Task<Reservacion> ObtenerPorIdAsync(int id);
        Task<Reservacion> ObtenerPorCodigoAsync(string codigo);
        Task<Reservacion> CrearAsync(Reservacion reservacion);
        Task ActualizarAsync(Reservacion reservacion);
        Task EliminarAsync(int id);
        Task<int> ObtenerTotalAsync();
        Task<bool> HabitacionDisponibleAsync(int numeroHabitacion, DateTime inicio, DateTime fin, int? excludeReservacionId = null);
    }
}