using AccessDB.Models;
using MVC.Interfaces;
using System.Net.Http.Json;
namespace MVC.Services
{
    public class CategoriaApiService : ICategoriaApiService
    {
        private readonly HttpClient _httpClient;
        public CategoriaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Categoria>> ObtenerTodasAsync()
        {
            var response = await _httpClient.GetAsync("api/CategoriaApi");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Categoria>>();
        }
        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/CategoriaApi/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Categoria>();
        }
    }
}
