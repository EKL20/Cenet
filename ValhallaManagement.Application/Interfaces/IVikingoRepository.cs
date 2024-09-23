using ValhallaManagement.Core.Entities;

namespace ValhallaManagement.Application.Interfaces
{
    public interface IVikingoRepository
    {
        Task AddAsync(Vikingo vikingo);
        Task<Vikingo> GetByIdAsync(int id);
        Task<IEnumerable<Vikingo>> GetAllAsync();
        Task UpdateAsync(Vikingo vikingo);
        Task DeleteAsync(int id);
    }
}
