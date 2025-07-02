using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceScheduling.Domain.Enums;

namespace ServiceScheduling.Domain.Entities;

[Table("user")]
public class User
{
    [Key] [Column("id")] public Guid Id { get; private set; }

    [Column("name")] [Required] public string Name { get; private set; }
    [Column("email")] [Required] public string Email { get; private set; }
    [Column("password")] [Required] public string Password { get; private set; }

    [ForeignKey("profileId")]
    [Column("profileId")]
    [Required]
    public int ProfileId { get; private set; }
    public Profile Profile { get; private set; }
    public ICollection<Service> Services { get; private set; } = new List<Service>();

    public User()
    {
    }

    public User(string name, string email, string password, int profileId)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        ProfileId = profileId;
    }

    public void AddService(Service service)
    {
        Services.Add(service);
    }

    public void RemoveService(Service service)
    {
        Services.Remove(service);
    }

    public void UpdateName(string newName)
    {
        Name = newName;
    }

    public void UpdateEmail(string newEmail)
    {
        Email = newEmail;
    }

    public void UpdatePassword(string newPassword)
    {
        Password = newPassword;
    }
}