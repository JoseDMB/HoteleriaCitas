using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccessDB.Models;
using API_Hotel.Interfaces;

namespace API_Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoHabitacionController : ControllerBase
    {
        private readonly ITipoHabitacionService _tipoHabitacionService;
        
        public TipoHabitacionController(ITipoHabitacionService tipoHabitacionService)
        {
            _tipoHabitacionService = tipoHabitacionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoHabitacion>>> GetAll()
        {
            var tiposHabitacion = await _tipoHabitacionService.ObtenerTodasAsync();
            return Ok(tiposHabitacion);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoHabitacion>> GetbyId(int id)
        {
            var tipoHabitacion = await _tipoHabitacionService.ObtenerPorIdAsync(id);
            if (tipoHabitacion == null) return NotFound();
            return Ok(tipoHabitacion);
        }

    }
}
