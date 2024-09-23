using ValhallaManagement.Application.Interfaces;
using ValhallaManagement.Core.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ValhallaManagement.Application.UseCase
{
    public class ObtenerVikingoPorIdUseCase
    {
        private readonly IVikingoRepository _vikingoRepository;
        private readonly ILogger<ObtenerVikingoPorIdUseCase> _logger;

        public ObtenerVikingoPorIdUseCase(IVikingoRepository vikingoRepository, ILogger<ObtenerVikingoPorIdUseCase> logger)
        {
            _vikingoRepository = vikingoRepository;
            _logger = logger;
        }

        public async Task<Vikingo> Execute(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Intento de recuperar un Vikingo con un ID inválido: {Id}", id);
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                _logger.LogInformation("Obteniendo el Vikingo con ID: {Id}", id);
                var vikingo = await _vikingoRepository.GetByIdAsync(id);
                if (vikingo == null)
                {
                    _logger.LogWarning("No se encontró un Vikingo con ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Vikingo recuperado con éxito con ID: {Id}", id);
                return vikingo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Vikingo con ID: {Id}", id);
                throw;
            }
        }
    }
}
