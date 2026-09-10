using AccessDB.Models;

namespace AccessDB
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente> GetByIdCardAsync(string cedula);
        Task<Cliente> GetByIdAsync(int id);
        Task<Cliente> AddClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int id);
        Task<int> totalClientesAsync();

    }
}
