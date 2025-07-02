using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Domain.Entities;
using ServiceScheduling.Domain.Interfaces;
using ServiceScheduling.Infra.Data;
using ServiceScheduling.Infrastructure.Data;

namespace ServiceScheduling.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken = default)
    {
        var emailExists = await _appDbContext.Users.AnyAsync(x => x.Email == user.Email, cancellationToken);

        if (emailExists) throw new InvalidOperationException("E-mail already exists.");

        await _appDbContext.Users.AddAsync(user, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<User>?> GetAllAsync(int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Users.AsNoTracking().Include(u => u.Profile).Include(u => u.Services).Skip(skip)
            .Take(take).ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Users.AsNoTracking().Include(u => u.Profile).Include(u => u.Services)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _appDbContext.Users.Update(user);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _appDbContext.Users.Remove(user);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}