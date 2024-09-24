using StackExchange.Redis;
using ValhallaManagement.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class FallbackCacheService : ICacheService
{
    private readonly ICacheService _sqliteCache;
    private readonly ICacheService _redisCache;
    private readonly ILogger<FallbackCacheService> _logger;
    private bool _useSqlite = true;  // Inicialmente intentamos usar SQLite

    public FallbackCacheService(ICacheService sqliteCache, ICacheService redisCache, ILogger<FallbackCacheService> logger)
    {
        _sqliteCache = sqliteCache;
        _redisCache = redisCache;
        _logger = logger;
    }

    public async Task<T> GetCachedDataAsync<T>(string key)
    {
        if (_useSqlite)
        {
            try
            {
                _logger.LogInformation("Intentando obtener datos desde SQLite");
                var sqliteData = await _sqliteCache.GetCachedDataAsync<T>(key);
                if (sqliteData != null)
                {
                    return sqliteData;
                }
            }
            catch (Exception ex) // Captura cualquier excepción de SQLite
            {
                _logger.LogError("Error de conexión con SQLite: {Message}. Cambiando a Redis.", ex.Message);
                _useSqlite = false; // Cambiar a Redis si SQLite falla
            }
        }

        // Fallback a Redis si SQLite falla
        _logger.LogInformation("Obteniendo datos desde Redis");
        return await _redisCache.GetCachedDataAsync<T>(key);
    }

    public async Task SetCachedDataAsync<T>(string key, T data, TimeSpan timeToLive)
    {
        if (_useSqlite)
        {
            try
            {
                _logger.LogInformation("Intentando guardar datos en SQLite");
                await _sqliteCache.SetCachedDataAsync(key, data, timeToLive);
            }
            catch (Exception ex) // Captura cualquier excepción de SQLite
            {
                _logger.LogError("Error de conexión con SQLite: {Message}. Cambiando a Redis.", ex.Message);
                _useSqlite = false; // Cambiar a Redis si SQLite falla
            }
        }

        // Fallback a Redis si SQLite falla
        _logger.LogInformation("Guardando datos en Redis");
        await _redisCache.SetCachedDataAsync(key, data, timeToLive);
    }

    public async Task RemoveCachedDataAsync(string key)
    {
        if (_useSqlite)
        {
            try
            {
                _logger.LogInformation("Intentando eliminar datos en SQLite");
                await _sqliteCache.RemoveCachedDataAsync(key);
            }
            catch (Exception ex) // Captura cualquier excepción de SQLite
            {
                _logger.LogError("Error de conexión con SQLite: {Message}. Cambiando a Redis.", ex.Message);
                _useSqlite = false; // Cambiar a Redis si SQLite falla
            }
        }

        // Fallback a Redis si SQLite falla
        _logger.LogInformation("Eliminando datos en Redis");
        await _redisCache.RemoveCachedDataAsync(key);
    }
}
