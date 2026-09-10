using AccessDB.Models;
using Microsoft.AspNetCore.Mvc;
using MVC.Interfaces;
using Proyecto1.ViewModels;

namespace Proyecto1.Controllers
{
    public class HabitacionesController : Controller
    {
        private readonly IHabitacionService _habitacionesService;
        private readonly ITipoHabitacionService _tipoHabitacion;

        public HabitacionesController(IHabitacionService habitacionesService, ITipoHabitacionService tipoHabitacion)
        {
            _habitacionesService = habitacionesService;
            _tipoHabitacion = tipoHabitacion;
        }


        public async System.Threading.Tasks.Task<IActionResult> Index(string buscar = null)
        {
            List<Habitacion> habitaciones;

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                // IHabitacionService no expone BuscarAsync por contrato; intentar ObtenerTodos y filtrar localmente
                var all = await _habitacionesService.ObtenerTodosAsync().ConfigureAwait(false);
                habitaciones = all.Where(h => h.NumeroHabitacion.ToString().Contains(buscar)).ToList();
                ViewData["BusquedaTermino"] = buscar;
            }
            else
            {
                habitaciones = await _habitacionesService.ObtenerTodosAsync().ConfigureAwait(false);
            }

            return View(habitaciones);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Categorias"] = await _tipoHabitacion.ObtenerTodasAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Create(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _habitacionesService.CrearAsync(habitacion).ConfigureAwait(false);
                    TempData["Exito"] = $"Habitación {habitacion.NumeroHabitacion} registrado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewData["Categorias"] = await _tipoHabitacion.ObtenerTodasAsync();

            return View(habitacion);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var habitacion = await _habitacionesService
                .ObtenerPorIdAsync(id.Value)
                .ConfigureAwait(false);

            if (habitacion == null)
                return NotFound();

            var vm = new HabitacionEditViewModel
            {
                Habitacion = habitacion,
                TiposHabitacion = await _tipoHabitacion.ObtenerTodasAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HabitacionEditViewModel model)
        {
            if (model?.Habitacion == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _habitacionesService
                        .ActualizarAsync(model.Habitacion)
                        .ConfigureAwait(false);

                    TempData["Exito"] =
                        $"La habitación {model.Habitacion.NumeroHabitacion} se ha actualizado exitosamente";

                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Recargar los tipos si volvemos a la vista
            model.TiposHabitacion = await _tipoHabitacion.ObtenerTodasAsync();

            return View(model);
        }

        public async System.Threading.Tasks.Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var habitacion = await _habitacionesService.ObtenerPorIdAsync(id.Value).ConfigureAwait(false);
            if (habitacion == null)
                return NotFound();

            return View(habitacion);
        }

        public async System.Threading.Tasks.Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var habitacion = await _habitacionesService.ObtenerPorIdAsync(id.Value).ConfigureAwait(false);
            if (habitacion == null)
                return NotFound();

            return View(habitacion);
        }

        /// <summary>
        /// Elimina un empleado confirmado
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> DeleteConfirmed(int id)
        {
                try
                {
                    await _habitacionesService.EliminarAsync(id).ConfigureAwait(false);
                    TempData["Exito"] = $"Habitacion eliminada exitosamente";
                }
                catch (InvalidOperationException ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            return RedirectToAction(nameof(Index));
        }
    }
}
