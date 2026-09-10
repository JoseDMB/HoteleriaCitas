using AccessDB.Models;
namespace MVC.Interfaces
{
    public interface ITipoHabitacionService
    {
        Task<List<TipoHabitacion>> ObtenerTodasAsync();
        Task<TipoHabitacion> ObtenerPorIdAsync(int id);
    }
}
