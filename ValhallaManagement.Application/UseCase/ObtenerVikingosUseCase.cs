using ValhallaManagement.Application.Interfaces;
using ValhallaManagement.Core.Entities;
using ValhallaManagement.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ValhallaManagement.Application.UseCases
{
    public class ObtenerVikingosUseCase
    {
        private readonly IVikingoRepository _vikingoRepository;
        private readonly ICacheService _cacheService;
        private readonly ILogger<ObtenerVikingosUseCase> _logger;
        private const string VikingosCacheKey = "vikingos";

        public ObtenerVikingosUseCase(IVikingoRepository vikingoRepository, ICacheService cacheService, ILogger<ObtenerVikingosUseCase> logger)
        {
            _vikingoRepository = vikingoRepository;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<IEnumerable<Vikingo>> Ejecutar()
        {
            try
            {
                // Verificamos si los datos están en caché
                _logger.LogInformation("Intentando obtener vikingos desde el caché con la clave: {VikingosCacheKey}", VikingosCacheKey);

                var cachedVikingos = await _cacheService.GetCachedDataAsync<IEnumerable<Vikingo>>(VikingosCacheKey);
                if (cachedVikingos != null)
                {
                    _logger.LogInformation("Datos de vikingos obtenidos del caché.");
                    return cachedVikingos;
                }

                // Si no están en caché, obtenemos los datos del repositorio
                _logger.LogInformation("No se encontraron datos en caché, obteniendo desde el repositorio.");

                var vikingos = await _vikingoRepository.GetAllAsync();
                if (vikingos == null || !vikingos.Any())
                {
                    _logger.LogWarning("No se encontraron vikingos en el repositorio.");
                    return new List<Vikingo>(); // Retornamos una lista vacía en lugar de nulo.
                }

                // Almacenamos los datos en la caché con una expiración de 1 hora
                _logger.LogInformation("Almacenando los datos de vikingos en caché.");
                await _cacheService.SetCachedDataAsync(VikingosCacheKey, vikingos, TimeSpan.FromHours(1));

                return vikingos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar obtener los vikingos.");
                throw; // Propagamos la excepción después de registrarla.
            }
        }
    }
}
