using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Domain.Entities;
using ServiceScheduling.Domain.Interfaces;
using ServiceScheduling.Infra.Data;

namespace ServiceScheduling.Infra.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public async Task SaveAsync(Service service, CancellationToken cancellationToken = default)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Service>?> GetAllAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default)
    {
        return await _context.Services.AsNoTracking().Include(provider => provider.Provider).Skip(skip).Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Services.AsNoTracking().Include(provider => provider.Provider)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Service service, CancellationToken cancellationToken = default)
    {
       _context.Services.Update(service);
       await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Service service, CancellationToken cancellationToken = default)
    {
        _context.Remove(service);
        await _context.SaveChangesAsync(cancellationToken);
    }
}