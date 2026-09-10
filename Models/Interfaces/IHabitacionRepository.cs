using AccessDB.Models;

namespace AccessDB
{
    public interface IHabitacionRepository
    {
        Task<List<Habitacion>> ObtenerTodosAsync();
        Task<Habitacion> GetHabitacionByNumeroAsync(int numero);
        Task<Habitacion> GetHabitacionByIdAsync(int id);
        Task<Habitacion> CreateAsync(Habitacion habitacion);
        Task UpdateAsync(Habitacion habitacion);
        Task DeleteAsync(int id);
        Task<int> totalHabitacionesAsync();
    }
}
