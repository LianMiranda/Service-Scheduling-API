using System.Linq.Expressions;
using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Application.Extensions;

public static class UserExtensions
{
    public static User ToEntity(this CreateUserDto dto)
    {
        return new User(dto.Name, dto.Email, dto.Password, dto.ProfileId);
    }
    
    public static ViewUserDto ToDto(this User user)
    {
        return new ViewUserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Profile = new ProfileDto { Role = user.Profile.Role },
            Services = user.Services.Select(s => new ServiceDto
                {
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl,
                    Provider = s.Provider,
                }
            ).ToList()
        };
    }
}