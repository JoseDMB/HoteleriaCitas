using API_Hotel.Interfaces;
using AccessDB.Models;
using AccessDB;

namespace API_Hotel.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<List<Categoria>> ObtenerTodasAsync()
        {
            return await _categoriaRepository.ObtenerTodasAsync();
        }

        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            return await _categoriaRepository.ObtenerPorIdAsync(id);
        }
    }
}
