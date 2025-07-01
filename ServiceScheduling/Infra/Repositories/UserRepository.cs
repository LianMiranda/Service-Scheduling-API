using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Common.Exceptions;
using ServiceScheduling.Domain.Entities;
using ServiceScheduling.Domain.Interfaces;
using ServiceScheduling.Infra.Data;

namespace ServiceScheduling.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ContextDatabase _context;

    public UserRepository(ContextDatabase context)
    {
        _context = context;
    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken = default)
    {
        var emailExists = await _context.Users.AnyAsync(x => x.Email == user.Email, cancellationToken);

        if (emailExists) throw new InvalidOperationException("E-mail already exists.");

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<User>?> GetAllAsync(int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users.AsNoTracking().Include(u => u.Profile).Include(u => u.Services).Skip(skip)
            .Take(take).ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AsNoTracking().Include(u => u.Profile).Include(u => u.Services)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Guid userId, UpdateUserDto user, CancellationToken cancellationToken = default)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (userExists == null) throw new NotFoundException($"user with id {userId} not found");
        if (!string.IsNullOrWhiteSpace(user.Name)) userExists.UpdateName(user.Name);
        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            var emailExists = await _context.Users.AnyAsync(x => x.Email == user.Email, cancellationToken);

            if (emailExists) throw new InvalidOperationException("E-mail already exists.");

            userExists.UpdateEmail(user.Email);
        }

        if (!string.IsNullOrWhiteSpace(user.Password)) userExists.UpdatePassword(user.Password);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FindAsync([userId], cancellationToken);
        if (user == null) throw new NotFoundException($"user with id {userId} not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}