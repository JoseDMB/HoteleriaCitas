using AccessDB.Models;
namespace MVC.Interfaces
{
    public interface IEmpleadoService
    {
        Task<List<Empleado>> ObtenerTodosAsync();
        Task<Empleado> ObtenerPorIdAsync(int id);
        Task<Empleado> ObtenerPorCedulaAsync(string cedula);
        Task<Empleado> CrearAsync(Empleado empleado);
        Task ActualizarAsync(Empleado empleado);
        Task EliminarAsync(int id);
        Task<int> ObtenerTotalAsync();
    }
}
