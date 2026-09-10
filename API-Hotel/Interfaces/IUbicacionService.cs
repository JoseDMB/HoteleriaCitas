using AccessDB.Models;

namespace API_Hotel.Interfaces
{
    public interface IUbicacionService
    {
        Task<List<Provincia>> GetProvinciasAsync();
        Task<Provincia> GetProvinciaByIdAsync(int id);
        Task<List<Distrito>> GetDistritoByCantonAsync(int id);
        Task<Distrito> GetDistritoByIdAsync(int id);
        Task<List<Canton>> GetCantonByProvinciaAsync(int id);
        Task<Canton> GetCantonByIdAsync(int id);
        Task<string> GetCompleteUbicationAsync(int provinciaId, int distritoId, int cantonId);
    }
}
