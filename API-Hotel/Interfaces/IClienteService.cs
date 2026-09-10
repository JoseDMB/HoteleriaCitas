using AccessDB.Models;

namespace API_Hotel.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<Cliente> BuscarPorCedulaAsync(string cedula);
        Task<Cliente> AddClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int id);
        Task<int> totalClientesAsync();
    }
}
