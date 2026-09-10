using AccessDB.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIdb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiClienteController : ControllerBase
    {
        public DBContext _context;

        public ApiClienteController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerClientes")]
        public List<Cliente> ObtenerClientes() { 
            var result = from c in _context.Cliente select c;
            return result.ToList();
        }

        [HttpPost("AgregarClientes")]
        public IActionResult AgregarClientes([FromBody] Cliente cliente) {
            _context.Cliente.Add(cliente);
            _context.SaveChanges();
            return Ok(cliente);

        }

        [HttpPut("Editar")]
        public IActionResult Editar([FromBody] Cliente cliente) {
            Cliente ClienteEditado;
            ClienteEditado = ObtenerCliente(cliente.Id);

            ClienteEditado.Nombre = cliente.Nombre;
            ClienteEditado.Cedula = cliente.Cedula;
            ClienteEditado.PrimerApellido = cliente.PrimerApellido;
            ClienteEditado.SegundoApellido = cliente.SegundoApellido;
            ClienteEditado.FechaNacimiento = cliente.FechaNacimiento;
            ClienteEditado.FechaRegistro = cliente.FechaRegistro;

            _context.Cliente.Update(ClienteEditado);
            _context.SaveChanges();
            return Ok(ClienteEditado);
        }

        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id) { 
            Cliente ClienteEliminar;
            ClienteEliminar = ObtenerCliente(id);
            if(ClienteEliminar == null)
            {
                return NotFound();
            }
            _context.Cliente.Remove(ClienteEliminar);
            _context.SaveChanges();
            return Ok(ClienteEliminar);
        }

        private Cliente ObtenerCliente(int id) {
            List<Cliente> lista;
            lista = ObtenerClientes();
            foreach (var persona in lista) 
            {
                if (persona.Id == id) 
                {
                    return persona;
                }
            }
            return null;
        }

    }
}
