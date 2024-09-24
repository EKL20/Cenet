using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ValhallaManagement.Blazor.Models;

namespace ValhallaManagement.Blazor.Services
{
    public class VikingoService : IVikingoService
    {
        private readonly HttpClient _httpClient;

        public VikingoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Guardar un nuevo vikingo
        public async Task SaveVikingo(VikingoFormModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/vikingos", model);
            response.EnsureSuccessStatusCode();
        }

        // Obtener un vikingo por su ID
        public async Task<VikingoFormModel> GetVikingoById(int id)
        {
            var response = await _httpClient.GetAsync($"api/vikingos/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VikingoFormModel>();
        }

        // Obtener la lista de todos los vikingos
        public async Task<List<VikingoFormModel>> GetAllVikingos()
        {
            var response = await _httpClient.GetAsync("api/vikingos");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<VikingoFormModel>>();
        }

        // Editar un vikingo existente
        public async Task UpdateVikingo(int id, VikingoFormModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/vikingos/{id}", model);
            response.EnsureSuccessStatusCode();
        }

        // Eliminar un vikingo por su ID
        public async Task DeleteVikingo(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/vikingos/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
