using ServiceScheduling.Application.DTOs;

namespace ServiceScheduling.Application.Interfaces;

public interface IUserService
{
    Task SaveAsync(CreateUserDto user, CancellationToken cancellationToken);
    Task<List<ViewUserDto>?> GetAllAsync(int skip, int take, CancellationToken cancellationToken);
    Task<ViewUserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, UpdateUserDto user, CancellationToken cancellationToken);
    Task DeleteAsync(Guid userId, CancellationToken cancellationToken);
}