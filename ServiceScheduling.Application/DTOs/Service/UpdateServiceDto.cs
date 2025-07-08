using Microsoft.AspNetCore.Http;

namespace ServiceScheduling.Application.DTOs.Service;

public class UpdateServiceDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public IFormFile? Image { get; set; }
    public string? ProviderId { get; set; }

    public UpdateServiceDto(string? name, string? description, double? price, IFormFile? image, string? providerId)
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