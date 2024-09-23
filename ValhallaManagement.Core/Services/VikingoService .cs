using ValhallaManagement.Core.Entities;
using ValhallaManagement.Core.Interfaces;
using ValhallaManagement.Core.Types;
using Microsoft.Extensions.Logging;

namespace ValhallaManagement.Core.Services
{
    public class VikingoService : IVikingoService
    {
        private readonly ILogger<VikingoService> _logger;

        public VikingoService(ILogger<VikingoService> logger)
        {
            _logger = logger;
        }

        // Método para calcular los puntos de Valhalla
        public int CalcularValhallaPoints(Vikingo vikingo)
        {
            try
            {
                _logger.LogInformation("Iniciando el cálculo de puntos de Valhalla para el vikingo con ID: {Id}", vikingo.Id);

                int puntos = vikingo.BatallasGanadas * 2;

                puntos += vikingo.NivelHonor switch
                {
                    NivelHonor.Bajo => 10,
                    NivelHonor.Medio => 50,
                    NivelHonor.Alto => 100,
                    _ => 0
                };

                _logger.LogInformation("Puntos calculados para el vikingo con ID: {Id} son: {Puntos}", vikingo.Id, puntos);
                return puntos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular los puntos de Valhalla para el vikingo con ID: {Id}", vikingo.Id);
                throw; // Lanzamos la excepción nuevamente después de registrarla
            }
        }

        // Método para clasificar el vikingo basado en los puntos de Valhalla y batallas ganadas
        public ClasificacionVikingo ClasificarVikingo(Vikingo vikingo)
        {
            try
            {
                _logger.LogInformation("Iniciando la clasificación del vikingo con ID: {Id}", vikingo.Id);

                if (vikingo.ValhallaPoints > 300 && vikingo.BatallasGanadas > 80)
                {
                    _logger.LogInformation("Clasificación asignada: Campeón del Valhalla para el vikingo con ID: {Id}", vikingo.Id);
                    return ClasificacionVikingo.CampeonDelValhalla;
                }

                if (vikingo.ValhallaPoints > 200)
                {
                    _logger.LogInformation("Clasificación asignada: Héroe Legendario para el vikingo con ID: {Id}", vikingo.Id);
                    return ClasificacionVikingo.HeroeLegendario;
                }

                if (vikingo.ValhallaPoints > 100 && vikingo.BatallasGanadas >= 30)
                {
                    _logger.LogInformation("Clasificación asignada: Guerrero Valiente para el vikingo con ID: {Id}", vikingo.Id);
                    return ClasificacionVikingo.GuerreroValiente;
                }

                if (vikingo.ValhallaPoints > 50)
                {
                    _logger.LogInformation("Clasificación asignada: Veterano Valeroso para el vikingo con ID: {Id}", vikingo.Id);
                    return ClasificacionVikingo.VeteranoValeroso;
                }

                _logger.LogInformation("Clasificación asignada: Destino Desconocido para el vikingo con ID: {Id}", vikingo.Id);
                return ClasificacionVikingo.Desconocido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al clasificar el vikingo con ID: {Id}", vikingo.Id);
                throw; // Lanzamos la excepción nuevamente después de registrarla
            }
        }
    }
}
