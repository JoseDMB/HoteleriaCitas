using AccessDB.Models;
using API_Hotel.Interfaces;
using API_Hotel.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitacionesApiController : ControllerBase
    {
        private readonly IHabitacionService _habitacionService;

        public HabitacionesApiController(IHabitacionService habitacionService)
        {
            _habitacionService = habitacionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Habitacion>>> GetAll()
        {
            return await _habitacionService.ObtenerTodosAsync();
        }

        [HttpGet("{numero}")]
        public async Task<ActionResult<Habitacion>> GetByNumero(int numero)
        {
            var h = await _habitacionService.GetHabitacionByNumeroAsync(numero);
            if (h == null) return NotFound();
            return h;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Habitacion habitacion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var creado = await _habitacionService.CreateAsync(habitacion);
                return CreatedAtAction(nameof(GetByNumero), new { numero = creado.NumeroHabitacion }, creado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Habitacion habitacion)
        {
            if (id != habitacion.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _habitacionService.UpdateAsync(habitacion);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var h = await _habitacionService.GetHabitacionByIdAsync(id);
            if (h == null) return NotFound();
            try
            {
                await _habitacionService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("cantidad")]
        public async Task<ActionResult<int>> GetCantidad()
        {
            var cantidad = await _habitacionService.totalHabitacionesAsync();
            return Ok(cantidad);
        }
    }
}
