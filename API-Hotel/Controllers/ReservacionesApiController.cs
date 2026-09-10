using AccessDB.Models;
using API_Hotel.Interfaces;
using API_Hotel.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservacionesApiController : ControllerBase
    {
        private readonly IReservacionService _reservacionService;

        public ReservacionesApiController(IReservacionService reservacionService)
        {
            _reservacionService = reservacionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservacion>>> GetAll()
        {
            return await _reservacionService.ObtenerTodosAsync();
        }

        [HttpGet("{id}", Name = "GetReservacion")]
        public async Task<ActionResult<Reservacion>> GetById(int id)
        {
            try
            {
                var reservacion = await _reservacionService.ObtenerPorIdAsync(id);
                return Ok(reservacion);
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<Reservacion>> GetByCodigo(string codigo)
        {
            try
            {
                var reservacion = await _reservacionService.ObtenerPorCodigoAsync(codigo);

                return Ok(reservacion);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Reservacion>> Create([FromBody] Reservacion reservacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//Preguntar

            try
            {
                var creado = await _reservacionService.CrearAsync(reservacion);

                return CreatedAtRoute("GetReservacion",new { id = creado.Id },creado);//Preguntar
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Reservacion reservacion)
        {
            if (id != reservacion.Id)
                return BadRequest(new { error = "El ID de la URL no coincide con el ID de la reservación." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _reservacionService.ActualizarAsync(reservacion);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _reservacionService.EliminarAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("cantidad")]
        public async Task<ActionResult<int>> GetCantidad()
        {
            var cantidad = await _reservacionService.ObtenerTotalAsync();
            return Ok(cantidad);
        }
    }
}

       