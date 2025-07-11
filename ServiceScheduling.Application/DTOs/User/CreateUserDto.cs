using System.ComponentModel.DataAnnotations;

namespace ServiceScheduling.Application.DTOs.User;

public class CreateUserDto
{
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Required] public int ProfileId { get; set; }

    public CreateUserDto(string name, string email, string password, int profileId)
    {
        Name = name;
        Email = email;
        Password = password;
        ProfileId = profileId;
    }
    
}