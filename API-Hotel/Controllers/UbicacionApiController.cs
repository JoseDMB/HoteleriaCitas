using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccessDB.Models;
using API_Hotel.Interfaces;

namespace API_Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionApiController : ControllerBase
    {
        private readonly IUbicacionService _ubicacionService;
        public UbicacionApiController(IUbicacionService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Provincia>>> GetAll()
        {
            var provincias = await _ubicacionService.GetProvinciasAsync();
            return Ok(provincias);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Provincia>> GetById(int id)
        {
            var provincia = await _ubicacionService.GetProvinciaByIdAsync(id);
            if (provincia == null) return NotFound();
            return Ok(provincia);
        }

        [HttpGet("distritos/{cantonId}")]
        public async Task<ActionResult<List<Distrito>>> GetDistritosByCanton(int cantonId)
        {
            var distritos = await _ubicacionService.GetDistritoByCantonAsync(cantonId);
            return Ok(distritos);
        }

        [HttpGet("distrito/{id}")]
        public async Task<ActionResult<Distrito>> GetDistritoById(int id)
        {
            var distrito = await _ubicacionService.GetDistritoByIdAsync(id);
            if (distrito == null) return NotFound();
            return Ok(distrito);
        }

        [HttpGet("cantones/{provinciaId}")]
        public async Task<ActionResult<List<Canton>>> GetCantonesByProvincia(int provinciaId)
        {
            var cantones = await _ubicacionService.GetCantonByProvinciaAsync(provinciaId);
            return Ok(cantones);
        }

        [HttpGet("canton/{id}")]
        public async Task<ActionResult<Canton>> GetCantonById(int id)
        {
            var canton = await _ubicacionService.GetCantonByIdAsync(id);
            if (canton == null) return NotFound();
            return Ok(canton);
        }

        [HttpGet("complete")]
        public async Task<ActionResult<string>> GetCompleteUbication(int provinciaId, int distritoId, int cantonId)
        {
            var completeUbication = await _ubicacionService.GetCompleteUbicationAsync(provinciaId, distritoId, cantonId);
            return Ok(completeUbication);
        }
    }
}
