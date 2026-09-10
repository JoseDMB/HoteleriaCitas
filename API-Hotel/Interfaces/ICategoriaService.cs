using AccessDB.Models;

namespace API_Hotel.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ObtenerTodasAsync();
        Task<Categoria> ObtenerPorIdAsync(int id);
    }
}
