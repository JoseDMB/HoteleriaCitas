using AccessDB.Models;
using MVC.Interfaces;
using System.Net.Http.Json;
namespace MVC.Services
{
    public class EstadoApiService : IEstadoApiService
    {
        private readonly HttpClient _httpClient;
        public EstadoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<EstadoReserva>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync("api/EstadoApi");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<EstadoReserva>>();
        }
        public async Task<EstadoReserva> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/EstadoApi/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EstadoReserva>();
        }
    }
}
