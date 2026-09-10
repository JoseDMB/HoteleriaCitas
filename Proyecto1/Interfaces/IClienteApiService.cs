using AccessDB.Models;
namespace MVC.Interfaces
{
    public interface IClienteApiService
    {
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente> ObtenerPorIdAsync(int id);
        Task<Cliente> ObtenerPorCedulaAsync(string cedula);
        Task<Cliente> CrearAsync(Cliente cliente);
        Task ActualizarAsync(Cliente cliente);
        Task EliminarAsync(int id);
        Task<int> ObtenerTotalAsync();
    }
}
