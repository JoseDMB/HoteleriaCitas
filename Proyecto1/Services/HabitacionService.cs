using AccessDB.Models;
using MVC.Interfaces;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
namespace MVC.Services
{
    public class HabitacionService: IHabitacionService
    {
        private readonly HttpClient _httpClient;
        public HabitacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Habitacion>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync("api/HabitacionesApi");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Habitacion>>();
        }

        public async Task<Habitacion> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/HabitacionesApi/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Habitacion>();
        }

        public async Task<Habitacion> ObtenerPorNumeroAsync(int numero)
        {
            var response = await _httpClient.GetAsync($"api/HabitacionesApi/{numero}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Habitacion>();
        }

        //Seguir con las validaciones de ID repetidos y sus try & catch
        public async Task<Habitacion> CrearAsync(Habitacion habitacion)
        {
            var response = await _httpClient.PostAsJsonAsync("api/HabitacionesApi", habitacion);
            if (response.IsSuccessStatusCode)
            {
                return habitacion;
            }
            
            var contenido = await response.Content.ReadAsStringAsync();
            string mensaje = $"Ya existe una habitacion con el numero: {habitacion.NumeroHabitacion}";

            try
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(contenido);
                if (!string.IsNullOrEmpty(error?.Error))
                {
                    mensaje = error.Error;
                }
            }
            catch
            {
                // Si no se puede leer el JSON, usamos el mensaje genérico
            }
            throw new InvalidOperationException(mensaje);
        }

        public async Task ActualizarAsync(Habitacion habitacion)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/HabitacionesApi/{habitacion.Id}", habitacion);
            response.EnsureSuccessStatusCode();
        }

        public async Task EliminarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/HabitacionesApi/{id}");

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var contenido = await response.Content.ReadAsStringAsync();

            string mensaje = "No se pudo eliminar la habitación porque tiene una reserva.";

            try
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(contenido);

                if (!string.IsNullOrEmpty(error?.Error))
                {
                    mensaje = error.Error;
                }
            }
            catch
            {
                // Si no se puede leer el JSON, usamos el mensaje genérico
            }

            throw new InvalidOperationException(mensaje);
        }

        public class ErrorResponse
        {
            public string? Error { get; set; }
        }

        public async Task<int> ObtenerTotalAsync()
        {
            var habitaciones = await _httpClient.GetAsync("api/HabitacionesApi/cantidad");
            habitaciones.EnsureSuccessStatusCode();
            return await habitaciones.Content.ReadFromJsonAsync<int>();
        }
    }
}
