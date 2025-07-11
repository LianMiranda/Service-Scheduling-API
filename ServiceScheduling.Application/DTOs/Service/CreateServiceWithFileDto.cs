using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ServiceScheduling.Application.DTOs.Service;

public class CreateServiceWithFileDto
{
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    [Required] public string Price { get; set; }
    public IFormFile? File { get; set; }
    [Required] public Guid ProviderId { get; set; }

    public CreateServiceWithFileDto(string name, string description, string price, IFormFile? file, Guid providerId)
    {
        Name = name;
        Description = description;
        Price = price;
        File = file;
        ProviderId = providerId;
    }

    public CreateServiceWithFileDto()
    {
    }
}