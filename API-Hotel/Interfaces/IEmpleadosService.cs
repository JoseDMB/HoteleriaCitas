using AccessDB.Models;

namespace API_Hotel.Interfaces
{
    public interface IEmpleadosService
    {
        Task<List<Empleado>> ObtenerTodosAsync();
        Task<Empleado> GetByIdCardAsync(string cedula);
        Task<Empleado> GetByIdAsync(int id);
        Task<Empleado> CreateAsync(Empleado empleado);
        Task UpdateAsync(Empleado empleado);
        Task DeleteAsync(int id);
        Task<int> totalEmpleadosAsync();
    }
}
