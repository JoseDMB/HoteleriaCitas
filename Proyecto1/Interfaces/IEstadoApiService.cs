using AccessDB.Models;
namespace MVC.Interfaces
{
    public interface IEstadoApiService
    {
        Task<List<EstadoReserva>> ObtenerTodosAsync();
        Task<EstadoReserva> ObtenerPorIdAsync(int id);
        
    }
}
