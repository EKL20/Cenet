using Microsoft.Data.Sqlite;
using System.Text.Json;
using ValhallaManagement.Core.Interfaces;

namespace ValhallaManagement.Infrastructure.Services
{
    public class SQLiteCacheService : ICacheService
    {
        private readonly string _connectionString;

        public SQLiteCacheService(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }

        // Método para inicializar la tabla de cache si no existe
        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            @"
            CREATE TABLE IF NOT EXISTS Cache (
                Key TEXT PRIMARY KEY,
                Value TEXT,
                Expiration DATETIME
            );
        ";
            command.ExecuteNonQuery();
        }

        public async Task<T> GetCachedDataAsync<T>(string key)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Value FROM Cache WHERE Key = @key AND (Expiration IS NULL OR Expiration > datetime('now'))";
            command.Parameters.AddWithValue("@key", key);

            var result = await command.ExecuteScalarAsync();
            return result != null ? JsonSerializer.Deserialize<T>(result.ToString()) : default;
        }

        public async Task SetCachedDataAsync<T>(string key, T data, TimeSpan timeToLive)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var expiration = DateTime.UtcNow.Add(timeToLive).ToString("yyyy-MM-dd HH:mm:ss");
            var serializedValue = JsonSerializer.Serialize(data);

            var command = connection.CreateCommand();
            command.CommandText = "INSERT OR REPLACE INTO Cache (Key, Value, Expiration) VALUES (@key, @value, @expiration)";
            command.Parameters.AddWithValue("@key", key);
            command.Parameters.AddWithValue("@value", serializedValue);
            command.Parameters.AddWithValue("@expiration", expiration);

            await command.ExecuteNonQueryAsync();
        }

        public async Task RemoveCachedDataAsync(string key)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Cache WHERE Key = @key";
            command.Parameters.AddWithValue("@key", key);

            await command.ExecuteNonQueryAsync();
        }
    }


}
