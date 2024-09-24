using ValhallaManagement.Application.Interfaces;
using ValhallaManagement.Core.Entities;
using ValhallaManagement.Core.Interfaces;
using ValhallaManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ValhallaManagement.Application.Interfaces.Impl
{
    public class VikingoRepository : IVikingoRepository
    {
        private readonly ApDbContext _context;
        private readonly ICacheService _cacheService;

        public VikingoRepository(ApDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task AddAsync(Vikingo vikingo)
        {
            _context.Vikingos.Add(vikingo);
            await _context.SaveChangesAsync();
            await _cacheService.RemoveCachedDataAsync("vikingos_all");
        }

        public async Task<Vikingo> GetByIdAsync(int id)
        {
            var cacheKey = $"vikingo:{id}";
            var vikingo = await _cacheService.GetCachedDataAsync<Vikingo>(cacheKey);
            if (vikingo == null)
            {
                vikingo = await _context.Vikingos.FindAsync(id);
                if (vikingo != null)
                {
                    await _cacheService.SetCachedDataAsync(cacheKey, vikingo, TimeSpan.FromMinutes(30));
                }
            }
            return vikingo;
        }

        public async Task<IEnumerable<Vikingo>> GetAllAsync()
        {
            var cacheKey = "vikingos_all";
            var vikingos = await _cacheService.GetCachedDataAsync<IEnumerable<Vikingo>>(cacheKey);
            if (vikingos == null)
            {
                vikingos = await _context.Vikingos.ToListAsync();
                await _cacheService.SetCachedDataAsync(cacheKey, vikingos, TimeSpan.FromMinutes(30));
            }
            return vikingos;
        }

        public async Task UpdateAsync(Vikingo vikingo)
        {
            _context.Vikingos.Update(vikingo);
            await _context.SaveChangesAsync();
            await _cacheService.RemoveCachedDataAsync($"vikingo:{vikingo.Id}");
            await _cacheService.RemoveCachedDataAsync("vikingos_all");
        }

        public async Task DeleteAsync(int id)
        {
            var vikingo = await _context.Vikingos.FindAsync(id);
            if (vikingo != null)
            {
                _context.Vikingos.Remove(vikingo);
                await _context.SaveChangesAsync();
                await _cacheService.RemoveCachedDataAsync($"vikingo:{id}");
                await _cacheService.RemoveCachedDataAsync("vikingos_all");
            }
        }
    }
}
