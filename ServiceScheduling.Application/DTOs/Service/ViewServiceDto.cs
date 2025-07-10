using System.Text.Json.Serialization;
using ServiceScheduling.Application.DTOs.User;
using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Application.DTOs.Service;

public class ViewServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }

    public ViewServiceDto()
    {
    }

    public ViewServiceDto(string name, string description, double price, string imageUrl)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }
}

public class ViewServiceWithProviderDto : ViewServiceDto
{
    [JsonPropertyOrder(99)] public ViewProviderDto Provider { get; set; }
}