using API_Hotel.Interfaces;
using AccessDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Hotel.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosApiController : ControllerBase
    {
        private readonly IEmpleadosService _empleadosService;

        public EmpleadosApiController(IEmpleadosService empleadosService)
        {
            _empleadosService = empleadosService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> GetAll()
        {
            return await _empleadosService.ObtenerTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetById(int id)
        {
            try 
            {
                var empleado = await _empleadosService.GetByIdAsync(id);
                return Ok(empleado);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("bycedula/{cedula}")]
        public async Task<ActionResult<Empleado>> GetByCedula(string cedula)
        {
            try 
            {
                var empleado = await _empleadosService.GetByIdCardAsync(cedula);
                return Ok(empleado);
            }
            catch 
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            try 
            {
                var creado = await _empleadosService.CreateAsync(empleado);
                return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { error = ex.Message, model = empleado });
            }
            catch (InvalidOperationException ex)
            {
                // Return a field-level validation error for Cedula and include the submitted model
                return BadRequest(new { errors = new { Cedula = ex.Message }, model = empleado });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message, model = empleado });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Empleado empleado)
        {
            if (id != empleado.Id) 
                return BadRequest();
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            try 
            {
                await _empleadosService.UpdateAsync(empleado);
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
            try
            {
                await _empleadosService.DeleteAsync(id);
                return NoContent();
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

        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalEmpleados()
        {
            var total = await _empleadosService.totalEmpleadosAsync();
            return total;
        }
       
    }
}
