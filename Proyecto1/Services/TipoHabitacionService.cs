using AccessDB.Models;
using MVC.Interfaces;
using System.Net.Http.Json;
namespace MVC.Services
{
    public class TipoHabitacionService : ITipoHabitacionService
    {
        private readonly HttpClient _httpClient;
        public TipoHabitacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<TipoHabitacion>> ObtenerTodasAsync()
        {
            var response = await _httpClient.GetAsync("api/TipoHabitacion");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TipoHabitacion>>();
        }

        public async Task<TipoHabitacion> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/TipoHabitacion/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TipoHabitacion>();
        }
    }
}
