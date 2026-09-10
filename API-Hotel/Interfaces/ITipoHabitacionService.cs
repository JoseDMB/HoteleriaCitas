using AccessDB.Models;

namespace API_Hotel.Interfaces
{
    public interface ITipoHabitacionService
    {
        Task<List<TipoHabitacion>> ObtenerTodasAsync();
        Task<TipoHabitacion> ObtenerPorIdAsync(int id);
    }
}
