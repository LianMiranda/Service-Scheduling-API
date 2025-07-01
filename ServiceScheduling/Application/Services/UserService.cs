using System.Security.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Common.Exceptions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task SaveAsync(CreateUserDto user, CancellationToken cancellationToken = default)
    {
        var properties = user.GetType().GetProperties();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(user);

            if (value == null)
                throw new ArgumentNullException($"{prop.Name}");


            if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value.ToString()))
                throw new ArgumentNullException($"{prop.Name}");
        }

        await _userRepository.SaveAsync(user.ToEntity(), cancellationToken);
    }

    public async Task<List<ViewUserDto>?> GetAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAllAsync(skip, take, cancellationToken);
        
        if (user == null || user.Count == 0) throw new NotFoundException("Users not found");
        
        return user.Select(u => u.ToDto()).ToList();
    }

    public async Task<ViewUserDto?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        return user.ToDto();
    }

    public async Task UpdateAsync(Guid id, UpdateUserDto user, CancellationToken cancellationToken)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _userRepository.UpdateAsync(id, user, cancellationToken);
    }

    public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(userId.ToString(), out var guid) == false)
        {
            throw new InvalidCredentialException("Invalid user id");
        }

        await _userRepository.DeleteAsync(userId, cancellationToken);
    }
}