using AccessDB;
using AccessDB.Models;
using API_Hotel.Interfaces;

namespace API_Hotel.Services
{
    public class EstadoResevService : IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository;
        public EstadoResevService(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }
        public async Task<List<EstadoReserva>> ObtenerTodasAsync()
        {
            return await _estadoRepository.ObtenerTodasAsync();
        }

        public async Task<EstadoReserva> ObtenerPorIdAsync(int id)
        {
            return await _estadoRepository.ObtenerPorIdAsync(id);
        }
    }
}