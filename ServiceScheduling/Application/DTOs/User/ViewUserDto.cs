using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Application.DTOs;

public class ViewUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ProfileDto Profile { get; set; }
    public ICollection<ServiceDto> Services { get; set; } = new List<ServiceDto>();
}