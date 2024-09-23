using ValhallaManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ValhallaManagement.Core.Interfaces;

namespace ValhallaManagement.Application.UseCases
{
    public class EliminarVikingoUseCase
    {
        private readonly IVikingoRepository _vikingoRepository;
        private readonly ICacheService _cacheService;
        private readonly ILogger<EliminarVikingoUseCase> _logger;
        private const string VikingosCacheKey = "vikingos";

        public EliminarVikingoUseCase(IVikingoRepository vikingoRepository, ICacheService cacheService, ILogger<EliminarVikingoUseCase> logger)
        {
            _vikingoRepository = vikingoRepository;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task Execute(int id)
        {
            // Validación de ID
            if (id <= 0)
            {
                _logger.LogWarning("Intento de eliminar un vikingo con un ID inválido: {Id}", id);
                throw new ArgumentException("El ID del vikingo debe ser mayor que 0.", nameof(id));
            }

            try
            {
                _logger.LogInformation("Iniciando la eliminación del vikingo con ID: {Id}", id);

                // Eliminamos el vikingo del repositorio
                await _vikingoRepository.DeleteAsync(id);

                _logger.LogInformation("Vikingo con ID: {Id} eliminado exitosamente.", id);

                // Invalida la caché después de eliminar un vikingo
                _logger.LogInformation("Invalidando el caché después de la eliminación del vikingo.");
                await _cacheService.RemoveCachedDataAsync(VikingosCacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar eliminar el vikingo con ID: {Id}", id);
                throw; // Propagamos la excepción después de registrarla
            }
        }
    }
}
