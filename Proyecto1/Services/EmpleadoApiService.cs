using AccessDB.Models;
using MVC.Interfaces;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
namespace MVC.Services
{
    public class EmpleadoApiService: IEmpleadoService
    {
        private readonly HttpClient _httpClient;
        public EmpleadoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Empleado>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync("api/EmpleadosApi");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Empleado>>();
        }
        public async Task<Empleado> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/EmpleadosApi/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Empleado>();
        }
        public async Task<Empleado> ObtenerPorCedulaAsync(string cedula)
        {
            var response = await _httpClient.GetAsync($"api/EmpleadosApi/bycedula/{cedula}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Empleado>();
        }
        public async Task<Empleado> CrearAsync(Empleado empleado)
        {
            var response = await _httpClient.PostAsJsonAsync("api/EmpleadosApi", empleado);

            if (response.IsSuccessStatusCode) {
                return empleado;
            }

            var content = await response.Content.ReadAsStringAsync();
            string mensaje = $"Ya existe un empleado con la cedula: {empleado.Cedula}";

            try
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(content);
                if (!string.IsNullOrEmpty(error?.Error))
                {
                    mensaje = error.Error;
                }
            }
            catch
            {

            }
            throw new InvalidOperationException(mensaje);
        }
        public async Task ActualizarAsync(Empleado empleado)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/EmpleadosApi/{empleado.Id}", empleado);
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
            var response = await _httpClient.DeleteAsync($"api/EmpleadosApi/{id}");
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
            var response = await _httpClient.GetAsync("api/EmpleadosApi/total");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public class ErrorResponse
        {
            public string? Error { get; set; }
        }
    }
}
