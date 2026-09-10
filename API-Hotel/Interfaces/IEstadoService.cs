using AccessDB.Models;

namespace API_Hotel.Interfaces
{
    public interface IEstadoService
    {
        Task<List<EstadoReserva>> ObtenerTodasAsync();
        Task<EstadoReserva> ObtenerPorIdAsync(int id);
    }
}
