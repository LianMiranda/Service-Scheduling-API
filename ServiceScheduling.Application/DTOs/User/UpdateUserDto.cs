namespace ServiceScheduling.Application.DTOs;

public class UpdateUserDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public UpdateUserDto(string? name, string? email, string? password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}