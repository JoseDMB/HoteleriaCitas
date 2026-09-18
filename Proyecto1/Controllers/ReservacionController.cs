using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        private readonly ITipoHabitacionService _tipoHabitacionService;
        public ReservacionController(IReservacionApiService reservacionService, IClienteApiService clienteService, IHabitacionService habitacionService, IEstadoApiService estadoResevService, ITipoHabitacionService tipoHabitacionService)
        {
            _reservacionService = reservacionService;
            _clienteService = clienteService;
            _habitacionesService = habitacionService;
            _estadoReservacionService = estadoResevService;
            _tipoHabitacionService = tipoHabitacionService;
        }



        public async Task<IActionResult> Index(string buscar = null)
        {
            // Load base data
            var reservaciones = await _reservacionService.ObtenerTodosAsync().ConfigureAwait(false);
            var clientes = await _clienteService.ObtenerTodosAsync().ConfigureAwait(false);
            var habitaciones = await _habitacionesService.ObtenerTodosAsync().ConfigureAwait(false);
            var estados = await _estadoReservacionService.ObtenerTodosAsync().ConfigureAwait(false);

            // Build lookup dictionaries
            var clientesById = clientes.ToDictionary(c => c.Id);
            var habitacionesById = habitaciones.ToDictionary(h => h.Id);
            var estadosById = estados.ToDictionary(e => e.Id);

            // Optional filtering
            if (!string.IsNullOrWhiteSpace(buscar))
            {
                reservaciones = reservaciones
                    .Where(r => r.CodigoReserva?.Contains(buscar, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();
                ViewData["BusquedaTermino"] = buscar;
            }

            // Project to view model
            var vm = reservaciones.Select(r => new Proyecto1.ViewModels.ReservacionIndexViewModel
            {
                Id = r.Id,
                CodigoReserva = r.CodigoReserva,
                ClienteNombre = clientesById.TryGetValue(r.IdCliente, out var c) ? $"{c?.Nombre} {c?.PrimerApellido} {c?.SegundoApellido}".Trim() : string.Empty,
                HabitacionNumero = habitacionesById.TryGetValue(r.IdHabitacion, out var h) ? h.NumeroHabitacion.ToString() : r.IdHabitacion.ToString(),
                FechaIngreso = r.FechaIngreso,
                FechaSalida = r.FechaSalida,
                TarifaReservacion = r.TarifaReservacion,
                Total = r.Total,
                EstadoReservacionId = r.EstadoReservacion,
                EstadoNombre = estadosById.TryGetValue(r.EstadoReservacion, out var es) ? es.Nombre : r.EstadoReservacion.ToString()
            }).ToList();

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Cliente"] = await _clienteService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["Habitacion"] = await _habitacionesService.ObtenerTodosAsync().ConfigureAwait(false);
            ViewData["EstadosReserva"] = await _estadoReservacionService.ObtenerTodosAsync();
            ViewData["TiposHabitacion"] = await _tipoHabitacionService.ObtenerTodasAsync().ConfigureAwait(false);
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
            ViewData["TiposHabitacion"] = await _tipoHabitacionService.ObtenerTodasAsync().ConfigureAwait(false);

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
                TempData["Exito"] = $"Reservación {reservacion.CodigoReserva} eliminada correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
