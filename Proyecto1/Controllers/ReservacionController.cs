using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AccessDB.Models;
using MVC.Interfaces;

namespace Proyecto1.Controllers
{
    public class ReservacionController : Controller
    {
        private readonly IReservacionApiService _reservacionService;
        private readonly IClienteApiService _clienteService;
        private readonly IHabitacionService _habitacionesService;
        private readonly IEstadoApiService _estadoReservacionService;
        public ReservacionController(IReservacionApiService reservacionService, IClienteApiService clienteService, IHabitacionService habitacionService, IEstadoApiService estadoResevService)
        {
            _reservacionService = reservacionService;
            _clienteService = clienteService;
            _habitacionesService = habitacionService;
            _estadoReservacionService = estadoResevService;
        }



        public async Task<IActionResult> Index(string buscar = null)
        {
            var reservaciones = await _reservacionService.ObtenerTodosAsync();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                reservaciones = reservaciones
                    .Where(r => r.CodigoReserva?.Contains(buscar, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();

                ViewData["BusquedaTermino"] = buscar;
            }

            return View(reservaciones);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Cliente"] = await _clienteService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["Habitacion"] = await _habitacionesService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["EstadosReserva"] = await _estadoReservacionService.ObtenerTodosAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var creado = await _reservacionService.CrearAsync(reservacion).ConfigureAwait(false);
                    TempData["Exito"] = $"Reservación creada exitosamente (Código: {creado.CodigoReserva})";
                    return RedirectToAction(nameof(Details), new { id = creado.Id });
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewData["Cliente"] = await _clienteService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["Habitacion"] = await _habitacionesService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["EstadosReserva"] = await _estadoReservacionService.ObtenerTodosAsync().ConfigureAwait(false);

            return View(reservacion);
        }

        public async System.Threading.Tasks.Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var reservacion = await _reservacionService.ObtenerPorIdAsync(id.Value).ConfigureAwait(false);
            if (reservacion == null)
                return NotFound();

            ViewData["Cliente"] = await _clienteService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["Habitacion"] = await _habitacionesService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["EstadosReserva"] =await _estadoReservacionService.ObtenerTodosAsync().ConfigureAwait(false);
            return View(reservacion);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Edit(Reservacion reservacion)
        {
            if (reservacion == null)
                return NotFound();

            if (reservacion.Id <= 0)
            {
                ModelState.AddModelError("", "Id de reservación inválido.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _reservacionService.ActualizarAsync(reservacion).ConfigureAwait(false);
                    TempData["Exito"] = $"La reservacion {reservacion.CodigoReserva} se ha actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewData["Cliente"] = await _clienteService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["Habitacion"] = await _habitacionesService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["EstadosReserva"] = await _estadoReservacionService.ObtenerTodosAsync().ConfigureAwait(false);

            return View(reservacion);
        }

     
        public async System.Threading.Tasks.Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var reservacion = await _reservacionService.ObtenerPorIdAsync(id.Value);
            if (reservacion == null)
                return NotFound();

            var cliente = await _clienteService.ObtenerPorIdAsync(reservacion.IdCliente);
            ViewBag.NombreCliente = cliente?.Nombre;

            return View(reservacion);
        }

        public async System.Threading.Tasks.Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var reservacion = await _reservacionService.ObtenerPorIdAsync(id.Value).ConfigureAwait(false);
            if (reservacion == null)
                return NotFound();

            return View(reservacion);
        }

        /// <summary>
        /// Elimina un empleado confirmado
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservacion = await _reservacionService.ObtenerPorIdAsync(id).ConfigureAwait(false);
            if (reservacion != null)
            {
                await _reservacionService.EliminarAsync(id).ConfigureAwait(false);
                TempData["Exito"] = $"Reservacion eliminada exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
