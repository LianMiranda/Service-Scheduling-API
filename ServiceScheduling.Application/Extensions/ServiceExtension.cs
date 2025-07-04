using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Application.DTOs.User;
using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Application.Extensions;

public static class ServiceExtension
{
    public static Service ToEntity(this CreateServiceDto dto)
    {
        return new Service(dto.Name, dto.Description, dto.Price, dto.ImageUrl!, dto.ProviderId);
    }

    public static ViewServiceDto ToDto(this Service service)
    {
        return new ViewServiceDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price,
            ImageUrl = service.ImageUrl,
            Provider = new ViewProviderDto
            {
                Name = service.Provider.Name,
            }
        };
    }
}