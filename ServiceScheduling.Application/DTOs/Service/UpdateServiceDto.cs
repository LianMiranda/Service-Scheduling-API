using Microsoft.AspNetCore.Http;

namespace ServiceScheduling.Application.DTOs.Service;

public class UpdateServiceDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Price { get; set; }
    public IFormFile? Image { get; set; }
    public string? ProviderId { get; set; }

    public UpdateServiceDto(string? name, string? description, string? price, IFormFile? image, string? providerId)
    {
        Name = name;
        Description = description;
        Price = price;
        Image = image;
        ProviderId = providerId;
    }

    public UpdateServiceDto()
    {
    }
}