using AccessDB.Models;
using Microsoft.AspNetCore.Mvc;
using MVC.Interfaces;

namespace Proyecto1.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteApiService _clienteApiService;

        public ClientesController(IClienteApiService clienteApiService)
        {
            _clienteApiService = clienteApiService;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteApiService.ObtenerTodosAsync();
            var cantidad = await _clienteApiService.ObtenerTotalAsync();
            ViewBag.TotalClientes = cantidad;
            return View(clientes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteApiService.ObtenerPorIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        public async Task<IActionResult> BuscarPorCedula(string cedula)
        {
            var cliente = await _clienteApiService.ObtenerPorCedulaAsync(cedula);
            if (cliente == null)
            {
                return NotFound();
            }
            return View("Details", cliente);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }
            try
            {
                await _clienteApiService.CrearAsync(cliente);

                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(cliente.Cedula), ex.Message);

                return View(cliente);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteApiService.ObtenerPorIdAsync(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }


        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(cliente);

            await _clienteApiService.ActualizarAsync(cliente);

            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var cliente = await _clienteApiService.ObtenerPorIdAsync(id.Value).ConfigureAwait(false);
            if (cliente == null)
                return NotFound();
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _clienteApiService.EliminarAsync(id);
                TempData["Success"] = "Cliente eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                // Mensaje de negocio (por ejemplo: tiene reservaciones)
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index)); ;
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al eliminar el cliente.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Clientes/Cantidad
        public async Task<IActionResult> Cantidad()
        {
            var cantidad = await _clienteApiService.ObtenerTotalAsync();

            return View(cantidad);
        }

    }
}
