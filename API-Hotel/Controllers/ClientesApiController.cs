using Microsoft.AspNetCore.Mvc;
using AccessDB.Models;
using API_Hotel.Interfaces;

namespace API_Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesApiController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesApiController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetAll()
        {
            return await _clienteService.ObtenerTodosAsync();
        }
        [HttpGet("cantidad")]
        public async Task<ActionResult<int>> GetCantidad()
        {
            var cantidad = await _clienteService.totalClientesAsync();
            return Ok(cantidad);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            try 
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id);
                return Ok(cliente);
            } 
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("bycedula/{cedula}")]
        public async Task<ActionResult<Cliente>> GetByCedula(string cedula)
        {
            try
            {
                var cliente = await _clienteService.BuscarPorCedulaAsync(cedula);
                return Ok(cliente);
            }
            catch
            {
                return NotFound();
            }   
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            try 
            {
                var creado = await _clienteService.AddClienteAsync(cliente);
                return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
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
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id) 
                return BadRequest(new { error = "El ID no coincide con el ID de ningun cliente." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try 
            {
                await _clienteService.UpdateClienteAsync(cliente);
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
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _clienteService.GetClienteByIdAsync(id);
            if (c == null) return NotFound();
            try
            {
                await _clienteService.DeleteClienteAsync(id);
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
        }

    }
}
