using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Domain.Interfaces;

public interface IServiceRepository
{
    Task SaveAsync(Service service, CancellationToken cancellationToken = default);
    Task<List<Service>?> GetAllAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
    Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Service service, CancellationToken cancellationToken = default);
    Task DeleteAsync(Service service, CancellationToken cancellationToken = default);
}