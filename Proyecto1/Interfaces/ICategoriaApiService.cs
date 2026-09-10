using AccessDB.Models;
namespace MVC.Interfaces
{
    public interface ICategoriaApiService
    {
        Task<List<Categoria>> ObtenerTodasAsync();
        Task<Categoria> ObtenerPorIdAsync(int id);
    }
}
