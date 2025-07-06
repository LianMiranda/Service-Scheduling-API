
using Microsoft.AspNetCore.Http;

namespace ServiceScheduling.Application.DTOs.Service;

public class CreateServiceWithFileDto
{
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile? File { get; set; }
        public Guid ProviderId { get; set; }

        public CreateServiceWithFileDto(string name, string description, double price, IFormFile? file, Guid providerId)
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