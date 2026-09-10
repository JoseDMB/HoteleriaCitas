using API_Hotel.Interfaces;
using AccessDB.Models;
using AccessDB;

namespace API_Hotel.Services
{
    public class UbicacionService : IUbicacionService
    {
        private readonly IUbicacionRepository _ubicacionRepository;
        public UbicacionService(IUbicacionRepository ubicacionRepository)
        {
            _ubicacionRepository = ubicacionRepository;
        }
        public async Task<List<Provincia>> GetProvinciasAsync()
        {
            return await _ubicacionRepository.GetProvinciasAsync();
        }
        public async Task<Provincia> GetProvinciaByIdAsync(int id)
        {
            return await _ubicacionRepository.GetProvinciaByIdAsync(id);
        }
        public async Task<List<Distrito>> GetDistritoByCantonAsync(int id)
        {
            return await _ubicacionRepository.GetDistritoByCantonAsync(id);
        }
        public async Task<Distrito> GetDistritoByIdAsync(int id)
        {
            return await _ubicacionRepository.GetDistritoByIdAsync(id);
        }
        public async Task<List<Canton>> GetCantonByProvinciaAsync(int id)
        {
            return await _ubicacionRepository.GetCantonByProvinciaAsync(id);
        }
        public async Task<Canton> GetCantonByIdAsync(int id)
        {
            return await _ubicacionRepository.GetCantonByIdAsync(id);
        }

        public async Task<string> GetCompleteUbicationAsync(int provinciaId, int distritoId, int cantonId)
        {
            return await _ubicacionRepository.GetCompleteUbicationAsync(provinciaId, distritoId, cantonId);
        }


    }
}
