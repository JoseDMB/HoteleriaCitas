using AccessDB.Models;

namespace AccessDB
{
    public interface IEstadoRepository
    {
        Task<List<EstadoReserva>> ObtenerTodasAsync();
        Task<EstadoReserva> ObtenerPorIdAsync(int id);
    }
}
