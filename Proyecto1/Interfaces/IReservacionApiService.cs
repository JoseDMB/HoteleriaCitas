using AccessDB.Models;
namespace MVC.Interfaces
{
    public interface IReservacionApiService
    {
        Task<List<Reservacion>> ObtenerTodosAsync();
        Task<Reservacion> ObtenerPorIdAsync(int id);
        Task<Reservacion> ObtenerPorCodigoAsync(string codigo);
        Task<Reservacion> CrearAsync(Reservacion reservacion);
        Task ActualizarAsync(Reservacion reservacion);
        Task EliminarAsync(int id);
        Task<int> ObtenerTotalAsync();
    }
}
