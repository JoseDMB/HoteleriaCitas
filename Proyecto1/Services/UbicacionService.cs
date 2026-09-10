using AccessDB.Models;
using MVC.Interfaces;
using System.Net.Http.Json;
namespace MVC.Services
{
    public class UbicacionService: IUbicacionService
    {
        private readonly HttpClient _httpClient;
        public UbicacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Provincia>> GetProvinciasAsync()
        {
            var response = await _httpClient.GetAsync("api/UbicacionApi");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Provincia>>();
        }

        public async Task<Provincia> GetProvinciaByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/UbicacionApi/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Provincia>();
        }

        public async Task<List<Distrito>> GetDistritoByCantonAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/UbicacionApi/distritos/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Distrito>>();
        }
        public async Task<Distrito> GetDistritoByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/UbicacionApi/distrito/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Distrito>();
        }

        public async Task<List<Canton>> GetCantonByProvinciaAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/UbicacionApi/cantones/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Canton>>();
        }

        public async Task<Canton> GetCantonByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/UbicacionApi/canton/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Canton>();
        }

        public async Task<string> GetCompleteUbicationAsync(int provinciaId, int distritoId, int cantonId)
        {
            var response = await _httpClient.GetAsync($"api/UbicacionApi/complete?provinciaId={provinciaId}&distritoId={distritoId}&cantonId={cantonId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
