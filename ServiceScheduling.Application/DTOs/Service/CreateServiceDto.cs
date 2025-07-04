namespace ServiceScheduling.Application.DTOs.Service;

public class CreateServiceDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string? ImageUrl { get; set; }
    public Guid ProviderId { get; set; }

    public CreateServiceDto(string name, string description, double price, string? imageUrl, Guid providerId)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        ProviderId = providerId;
    }
}