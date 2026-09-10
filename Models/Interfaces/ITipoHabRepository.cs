using AccessDB.Models;

namespace AccessDB
{
    public interface ITipoHabRepository
    {
        Task<List<TipoHabitacion>> ObtenerTodasAsync();
        Task<TipoHabitacion> ObtenerPorIdAsync(int id);
    }
}
