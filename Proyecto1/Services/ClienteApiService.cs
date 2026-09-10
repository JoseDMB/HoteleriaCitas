using AccessDB.Models;
using MVC.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
namespace MVC.Services
{
    public class ClienteApiService : IClienteApiService
    {
        private readonly HttpClient _httpClient;
        public ClienteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync("api/ClientesApi");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Cliente>>();
        }
        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/ClientesApi/{id}");
            response.EnsureSuccessStatusCode(); 
            return await response.Content.ReadFromJsonAsync<Cliente>();
        }
        public async Task<Cliente> ObtenerPorCedulaAsync(string cedula)
        {
            var response = await _httpClient.GetAsync($"api/ClientesApi/bycedula/{cedula}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Cliente>();
        }
        public async Task<Cliente> CrearAsync(Cliente cliente)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ClientesApi/crear", cliente);

            if (response.IsSuccessStatusCode) 
            {
                return cliente;
            }

            var contenido = await response.Content.ReadAsStringAsync();
            string mensaje = $"Ya existe un cliente con la cédula {cliente.Cedula}";

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
        public async Task ActualizarAsync(Cliente cliente)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ClientesApi/{cliente.Id}", cliente);
            response.EnsureSuccessStatusCode();
        }
        public async Task EliminarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ClientesApi/{id}");
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            var contenido = await response.Content.ReadAsStringAsync();
            // Intentamos leer el mensaje de error enviado por la API (ej: { error: "..." })
            string errorMessage = "No se puede eliminar el cliente porque tiene reservaciones.";
            try
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(contenido);
                if (!string.IsNullOrEmpty(error?.Error))
                {
                    errorMessage = error.Error;
                }
            }
            catch
            {
                // Ignorar fallos al parsear el cuerpo de error
            }

            throw new InvalidOperationException(errorMessage);
        }
        public class ErrorResponse
        {
            public string? Error { get; set; }
        }
        public async Task<int> ObtenerTotalAsync()
        {
            var response = await _httpClient.GetAsync("api/ClientesApi/cantidad");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}
