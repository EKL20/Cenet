using ValhallaManagement.Application.Interfaces;
using ValhallaManagement.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ValhallaManagement.Core.Interfaces;

namespace ValhallaManagement.Application.UseCase
{
    public class ActualizarVikingoUseCase
    {
        private readonly IVikingoRepository _vikingoRepository;
        private readonly ICacheService _cacheService;
        private readonly ILogger<ActualizarVikingoUseCase> _logger;
        private const string VikingosCacheKey = "vikingos";

        public ActualizarVikingoUseCase(IVikingoRepository vikingoRepository, ICacheService cacheService, ILogger<ActualizarVikingoUseCase> logger)
        {
            _vikingoRepository = vikingoRepository;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task Execute(Vikingo vikingo)
        {
            // Validaciones
            if (vikingo == null)
            {
                _logger.LogWarning("Se intentó actualizar un vikingo nulo.");
                throw new ArgumentNullException(nameof(vikingo), "El vikingo no puede ser nulo.");
            }

            if (vikingo.Id <= 0)
            {
                _logger.LogWarning("Intento de actualizar un vikingo con ID no válido: {Id}", vikingo.Id);
                throw new ArgumentException("El ID del vikingo debe ser mayor que 0.", nameof(vikingo.Id));
            }

            try
            {
                _logger.LogInformation("Actualizando el vikingo con ID: {Id}", vikingo.Id);

                // Actualizar el vikingo en el repositorio
                await _vikingoRepository.UpdateAsync(vikingo);

                _logger.LogInformation("Vikingo con ID: {Id} actualizado exitosamente.", vikingo.Id);

                // Invalida la caché después de una actualización exitosa
                _logger.LogInformation("Invalidando el caché después de actualizar el vikingo.");
                await _cacheService.RemoveCachedDataAsync(VikingosCacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el vikingo con ID: {Id}", vikingo.Id);
                throw; // Propagamos la excepción después de registrarla
            }
        }
    }
}
