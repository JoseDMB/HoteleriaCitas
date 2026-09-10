using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Hotel.Interfaces;
using AccessDB.Models;
using AccessDB;

namespace API_Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoApiController : ControllerBase
    {
        private readonly IEstadoService _estadoService;

        public EstadoApiController(IEstadoService estadoService) {
            _estadoService = estadoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstados()
        {
            var estados = await _estadoService.ObtenerTodasAsync();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstadoById(int id)
        {
            try
            {
                var estado = await _estadoService.ObtenerPorIdAsync(id);
                return Ok(estado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
