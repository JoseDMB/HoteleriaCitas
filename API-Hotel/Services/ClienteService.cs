using AccessDB.Models;

using Microsoft.EntityFrameworkCore;
using AccessDB;
using API_Hotel.Interfaces;

namespace API_Hotel.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IReservacionRepository _reservacionRepository;

        public ClienteService(IClienteRepository clienteRepository, IReservacionRepository reservacionRepository)
        {
            _clienteRepository = clienteRepository;
            _reservacionRepository = reservacionRepository;
        }

        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            try
            {
                return await _clienteRepository.ObtenerTodosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los clientes.", ex);
            }
        }
        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);
                if (cliente == null)
                {
                    throw new Exception("Cliente no encontrado");
                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el cliente.", ex);
            }
        }

        public async Task<Cliente> BuscarPorCedulaAsync(string cedula)
        {
            try
            {
                var cliente = await _clienteRepository.GetByIdCardAsync(cedula);
                if (cliente == null)
                {
                    throw new Exception("Cliente no encontrado");
                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el cliente por cédula.", ex);
            }
        }

        public async Task<Cliente> AddClienteAsync(Cliente cliente)
        {
            try
            {
                var existingCliente = await _clienteRepository.GetByIdCardAsync(cliente.Cedula);
                if (existingCliente != null)
                {
                    throw new InvalidOperationException("Esta cédula ya esta registrada");
                }
                return await _clienteRepository.AddClienteAsync(cliente);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cliente", ex);
            }
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            try
            {
                var existingCliente = await _clienteRepository.GetByIdAsync(cliente.Id);
                if (existingCliente == null)
                {
                    throw new Exception("Cliente no encontrado");
                }
                var clienteConMismaCedula = await _clienteRepository.GetByIdCardAsync(cliente.Cedula);

                if (clienteConMismaCedula != null && clienteConMismaCedula.Id != cliente.Id)
                {
                    throw new InvalidOperationException(
                        "Esta cédula ya está registrada por otro cliente.");
                }
                existingCliente.Nombre = cliente.Nombre;
                existingCliente.Cedula = cliente.Cedula;
                existingCliente.PrimerApellido = cliente.PrimerApellido;
                existingCliente.SegundoApellido = cliente.SegundoApellido;
                existingCliente.FechaNacimiento = cliente.FechaNacimiento;
                await _clienteRepository.UpdateClienteAsync(existingCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el cliente.", ex);
            }
        }
        public async Task DeleteClienteAsync(int id)
        {
            try 
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);
                if (cliente == null)
                {
                    throw new KeyNotFoundException("Cliente no encontrado");
                }

                bool tieneReservaciones = await _reservacionRepository.ExisteReservacionPorClienteAsync(id);
                if (tieneReservaciones)
                {
                    // Lanzar InvalidOperationException para que el controlador lo capture y muestre un mensaje al usuario
                    throw new InvalidOperationException("No se puede eliminar el cliente porque tiene reservaciones a su nombre.");
                }

                await _clienteRepository.DeleteClienteAsync(id);
            } catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente.", ex);
            }
        }

        public async Task<int> totalClientesAsync()
        {
            try
            {
                return await _clienteRepository.totalClientesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de clientes.", ex);
            }
        }
    }
}
