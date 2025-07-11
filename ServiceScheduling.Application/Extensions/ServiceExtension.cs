using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Application.DTOs.User;
using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Application.Extensions;

public static class ServiceExtension
{
    public static Service ToEntity(this CreateServiceDto dto, double price)
    {
        return new Service(dto.Name, dto.Description, price, dto.ImageUrl!, dto.ProviderId);
    }

    public static CreateServiceDto ToCreateServiceDto(this CreateServiceWithFileDto dto, string? imageUrl)
    {
        return new CreateServiceDto(
            dto.Name,
            dto.Description,
            dto.Price,
            imageUrl,
            dto.ProviderId
        );
    }

    public static ViewServiceWithProviderDto ToDto(this Service service)
    {
        return new ViewServiceWithProviderDto()
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