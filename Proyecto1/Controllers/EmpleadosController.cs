using AccessDB.Models;
using Microsoft.AspNetCore.Mvc;
using MVC.Interfaces;
using MVC.Services;
using MVC;

namespace Proyecto1.Controllers
{
    public class EmpleadosController : Controller
    {

        private readonly IEmpleadoService _empleadoService;
        private readonly IUbicacionService _ubicacionService;
        private readonly ICategoriaApiService _categoriaApiService;

        public EmpleadosController(IEmpleadoService empleadoService, IUbicacionService ubicacionService, ICategoriaApiService categoriaApiService)
        {
            _empleadoService = empleadoService;
            _ubicacionService = ubicacionService;
            _categoriaApiService = categoriaApiService;
        }

        public async Task<IActionResult> Index()
        {
            var empleados = await _empleadoService.ObtenerTodosAsync();
            var cantidad = await _empleadoService.ObtenerTotalAsync();
            ViewBag.TotalEmpleados = cantidad;
            return View(empleados);
        }



     
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var provincias = await _ubicacionService.GetProvinciasAsync();
            var categorias = await _categoriaApiService.ObtenerTodasAsync();
            ViewBag.Categorias = categorias;
            ViewBag.Provincias = provincias;

            // También establecer ViewData para compatibilidad con vistas existentes
            ViewData["Categorias"] = categorias;
            ViewData["Provincias"] = provincias;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                await _empleadoService.CrearAsync(empleado);
                return RedirectToAction("Index");
            }
            // Si hay errores, volver a poblar datos necesarios para la vista
            var provincias = await _ubicacionService.GetProvinciasAsync();
            var categorias = await _categoriaApiService.ObtenerTodasAsync();
            ViewBag.Categorias = categorias;
            ViewBag.Provincias = provincias;
            ViewData["Categorias"] = categorias;
            ViewData["Provincias"] = provincias;
            return View(empleado);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var empleado = await _empleadoService.ObtenerPorIdAsync(id);
            if (empleado == null)
                return NotFound();

            return View(empleado);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var empleado = await _empleadoService.ObtenerPorIdAsync(id);
            if (empleado == null)
                return NotFound();

            var provincias = await _ubicacionService.GetProvinciasAsync();
            var categorias = await _categoriaApiService.ObtenerTodasAsync();
            ViewBag.Categorias = categorias;
            ViewBag.Provincias = provincias;
            ViewData["Categorias"] = categorias;
            ViewData["Provincias"] = provincias;

            return View(empleado);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empleado empleado)
        {
            if (id != empleado.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                // Re-poblar datos necesarios para re-renderizar la vista
                var provincias = await _ubicacionService.GetProvinciasAsync();
                var categorias = await _categoriaApiService.ObtenerTodasAsync();
                ViewBag.Categorias = categorias;
                ViewBag.Provincias = provincias;
                ViewData["Categorias"] = categorias;
                ViewData["Provincias"] = provincias;
                return View(empleado);
            }

            await _empleadoService.ActualizarAsync(empleado);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _empleadoService.ObtenerPorIdAsync(id);

            if (empleado == null)
                return NotFound();

            return View(empleado);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _empleadoService.EliminarAsync(id);

            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Cantidad
        public async Task<IActionResult> Cantidad()
        {
            var cantidad = await _empleadoService.ObtenerTotalAsync();

            return View(cantidad);
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerCantones(int provinciaId)
        {
            var cantones = await _ubicacionService.GetCantonByProvinciaAsync(provinciaId);

            return Json(cantones.Select(c => new
            {
                id = c.Id,
                nombre = c.Nombre
            }));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDistritos(int cantonId)
        {
            var distritos = await _ubicacionService.GetDistritoByCantonAsync(cantonId);

            return Json(distritos.Select(d => new
            {
                id = d.Id,
                nombre = d.Nombre
            }));
        }

    }
}
