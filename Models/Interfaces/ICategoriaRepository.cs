using AccessDB.Models;
namespace AccessDB
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObtenerTodasAsync();
        Task<Categoria> ObtenerPorIdAsync(int id);
    }
}
