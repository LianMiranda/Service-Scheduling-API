namespace ServiceScheduling.Application.DTOs.User;

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int ProfileId { get; set; }

    public CreateUserDto(string name, string email, string password, int profileId)
    {
        Name = name;
        Email = email;
        Password = password;
        ProfileId = profileId;
    }
    
}