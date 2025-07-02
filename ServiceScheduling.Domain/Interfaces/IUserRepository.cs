using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Domain.Interfaces;

public interface IUserRepository
{
    Task SaveAsync(User user, CancellationToken cancellationToken = default);
    Task<List<User>?> GetAllAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(User? user, CancellationToken cancellationToken = default);
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);
}