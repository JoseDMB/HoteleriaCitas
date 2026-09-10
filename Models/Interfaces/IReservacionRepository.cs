using AccessDB.Models;

namespace AccessDB
{
    public interface IReservacionRepository
    {
        Task<List<Reservacion>> ObtenerTodosAsync();
        Task<Reservacion> ObtenerPorCodigoAsync(string codigo);
        Task<Reservacion> ObtenerPorIdAsync(int id);
        Task<Reservacion> CrearAsync(Reservacion reservacion);
        Task ActualizarAsync(Reservacion reservacion);
        Task EliminarAsync(int id);
        Task<int> ObtenerTotalAsync();
        Task<bool> HabitacionDisponibleAsync(int numeroHabitacion, DateTime inicio, DateTime fin, int? excludeReservacionId = null);
        Task<bool> ExisteReservacionPorClienteAsync(int idCliente);
        Task<bool> ExisteReservacionPorHabitacionAsync(int idHabitacion);
    }
}