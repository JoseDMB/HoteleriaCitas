using AccessDB.Models;

namespace API_Hotel.Interfaces
{
    public interface IHabitacionService
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
