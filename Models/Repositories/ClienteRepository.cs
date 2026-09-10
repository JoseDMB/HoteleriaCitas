using AccessDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDB
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DBContext _clientes;
        public ClienteRepository(DBContext clientes)
        {
            _clientes = clientes;
        }

        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
             return await _clientes.Cliente.AsNoTracking().OrderByDescending(c => c.FechaRegistro).ToListAsync();   
        }
        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _clientes.Cliente.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Cliente> GetByIdCardAsync(string cedula)
        {
                return await _clientes.Cliente.AsNoTracking().FirstOrDefaultAsync(c => c.Cedula == cedula);
        }

        public async Task<Cliente> AddClienteAsync(Cliente cliente)
        {
            await _clientes.Cliente.AddAsync(cliente);
            await _clientes.SaveChangesAsync();
            return cliente;
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _clientes.Cliente.Update(cliente);
            await _clientes.SaveChangesAsync();

        }
        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _clientes.Cliente.FirstOrDefaultAsync(c => c.Id == id);
            _clientes.Cliente.Remove(cliente);
            await _clientes.SaveChangesAsync();
        }
        public async Task<int> totalClientesAsync()
        {
            return await _clientes.Cliente.CountAsync();
        }
        
    }
}
