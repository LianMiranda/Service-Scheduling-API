using ServiceScheduling.Application.DTOs.Profile;
using ServiceScheduling.Application.DTOs.Service;

namespace ServiceScheduling.Application.DTOs.User;

public class ViewUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ProfileDto Profile { get; set; }
    public ICollection<ViewServiceDto> Services { get; set; } = new List<ViewServiceDto>();
}