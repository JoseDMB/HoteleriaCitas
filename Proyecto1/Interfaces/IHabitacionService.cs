using AccessDB.Models;
namespace MVC.Interfaces
{
    public interface IHabitacionService
    {
        Task<List<Habitacion>> ObtenerTodosAsync();
        Task<Habitacion> ObtenerPorIdAsync(int id);
        Task<Habitacion> ObtenerPorNumeroAsync(int numero);
        Task<Habitacion> CrearAsync(Habitacion habitacion);
        Task ActualizarAsync(Habitacion habitacion);
        Task EliminarAsync(int id);
        Task<int> ObtenerTotalAsync();

    }
}
