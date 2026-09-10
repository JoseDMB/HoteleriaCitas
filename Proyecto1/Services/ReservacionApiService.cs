using AccessDB.Models;
using MVC.Interfaces;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
namespace MVC.Services
{
    public class ReservacionApiService : IReservacionApiService
    {
        private readonly HttpClient _httpClient;

        public ReservacionApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Reservacion>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync("api/ReservacionesApi");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Reservacion>>();
        }

        public async Task<Reservacion> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/ReservacionesApi/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Reservacion>();
        }

        public async Task<Reservacion> ObtenerPorCodigoAsync(string codigo)
        {
            var response = await _httpClient.GetAsync($"api/ReservacionesApi/codigo/{codigo}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Reservacion>();
        }

        public async Task<Reservacion> CrearAsync(Reservacion reservacion)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ReservacionesApi", reservacion);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Reservacion>();

            // Intentar leer mensaje de error devuelto por la API
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                var doc = JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("error", out var err))
                    throw new InvalidOperationException(err.GetString());
            }
            catch (JsonException) { }

            throw new InvalidOperationException(content);
        }

        public async Task ActualizarAsync(Reservacion reservacion)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ReservacionesApi/{reservacion.Id}", reservacion);
            if (response.IsSuccessStatusCode)
                return;

            var content = await response.Content.ReadAsStringAsync();
            try
            {
                var doc = JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("error", out var err))
                    throw new InvalidOperationException(err.GetString());
            }
            catch (JsonException) { }

            throw new InvalidOperationException(content);
        }

        public async Task EliminarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ReservacionesApi/{id}");
            if (response.IsSuccessStatusCode)
                return;

            var content = await response.Content.ReadAsStringAsync();
            try
            {
                var doc = JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("error", out var err))
                    throw new InvalidOperationException(err.GetString());
            }
            catch (JsonException) { }

            throw new InvalidOperationException(content);
        }

        public async Task<int> ObtenerTotalAsync()
        {
            var response = await _httpClient.GetAsync("api/ReservacionesApi/cantidad");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}
