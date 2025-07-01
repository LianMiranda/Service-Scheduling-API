using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Domain.Interfaces;

public interface IUserRepository
{
    Task SaveAsync(User user, CancellationToken cancellationToken = default);
    Task<List<User>?> GetAllAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, UpdateUserDto user, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default);
}