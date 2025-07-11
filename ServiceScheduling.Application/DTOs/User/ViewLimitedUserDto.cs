namespace ServiceScheduling.Application.DTOs.User;

public class ViewLimitedUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }

    public ViewLimitedUserDto(string name, string email)
    {
        Name = name;
        Email = email;
    }
}