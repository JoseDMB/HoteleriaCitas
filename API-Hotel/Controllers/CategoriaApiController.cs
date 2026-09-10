using Microsoft.AspNetCore.Mvc;
using AccessDB.Models;
using API_Hotel.Interfaces;
namespace API_Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaApiController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaApiController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }


        [HttpGet]
        public async Task<List<Categoria>> GetCategorias()
        {
            return await _categoriaService.ObtenerTodasAsync();
            
        }


        [HttpGet("{id}")]
        public async Task<Categoria> GetCategoriaById(int id)
        {
            return await _categoriaService.ObtenerPorIdAsync(id);
        }
    }
}
