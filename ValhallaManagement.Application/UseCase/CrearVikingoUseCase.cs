using ValhallaManagement.Application.DTOs;
using ValhallaManagement.Application.Interfaces;
using ValhallaManagement.Core.Entities;
using ValhallaManagement.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ValhallaManagement.Application.UseCase
{
    public class CrearVikingoUseCase
    {
        private readonly IVikingoRepository _vikingoRepository;
        private readonly ICacheService _cacheService;
        private readonly IVikingoService _vikingoService;
        private readonly ILogger<CrearVikingoUseCase> _logger;
        private const string VikingosCacheKey = "vikingos";

        public CrearVikingoUseCase(IVikingoRepository vikingoRepository, IVikingoService vikingoService, ICacheService cacheService, ILogger<CrearVikingoUseCase> logger)
        {
            _vikingoRepository = vikingoRepository;
            _vikingoService = vikingoService;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<Vikingo> Ejecutar(VikingoDto vikingoDto)
        {
            // Validamos los datos de entrada
            if (vikingoDto == null)
            {
                _logger.LogWarning("Intento de crear un vikingo con datos nulos.");
                throw new ArgumentNullException(nameof(vikingoDto), "Los datos del vikingo no pueden ser nulos.");
            }

            if (string.IsNullOrWhiteSpace(vikingoDto.Nombre))
            {
                _logger.LogWarning("Intento de crear un vikingo con un nombre vacío.");
                throw new ArgumentException("El nombre del vikingo es obligatorio.", nameof(vikingoDto.Nombre));
            }

            try
            {
                // Mapeo de DTO a entidad
                _logger.LogInformation("Creando un vikingo con nombre: {Nombre}", vikingoDto.Nombre);

                var vikingo = new Vikingo
                {
                    Nombre = vikingoDto.Nombre,
                    BatallasGanadas = vikingoDto.BatallasGanadas,
                    ArmaFavorita = vikingoDto.ArmaFavorita,
                    NivelHonor = vikingoDto.NivelHonor,
                    CausaMuerteGloriosa = vikingoDto.CausaMuerteGloriosa
                };

                // Calculamos los puntos de Valhalla y la clasificación
                vikingo.ValhallaPoints = _vikingoService.CalcularValhallaPoints(vikingo);
                vikingo.Clasificacion = _vikingoService.ClasificarVikingo(vikingo);

                // Guardamos el vikingo en el repositorio
                await _vikingoRepository.AddAsync(vikingo);
                _logger.LogInformation("Vikingo creado exitosamente con nombre: {Nombre} y ID: {Id}", vikingo.Nombre, vikingo.Id);

                // Invalida la caché después de la creación exitosa
                _logger.LogInformation("Invalidando el caché después de la creación de un nuevo vikingo.");
                await _cacheService.RemoveCachedDataAsync(VikingosCacheKey);

                return vikingo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el vikingo con nombre: {Nombre}", vikingoDto.Nombre);
                throw; // Lanzamos nuevamente la excepción después de registrarla
            }
        }
    }
}
