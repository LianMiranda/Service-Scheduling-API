namespace ServiceScheduling.Application.DTOs.Service;

public class UpdateServiceDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? ProviderId { get; set; }

    public UpdateServiceDto(string? name, string? description, double? price, string? imageUrl, string? providerId)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        ProviderId = providerId;
    }
}