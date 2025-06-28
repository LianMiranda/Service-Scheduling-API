using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceScheduling.Domain.Enums;

namespace ServiceScheduling.Domain.Entities;

[Table("profile")]
public class Profile
{
    [Key] public int Id { get; private set; }

    [Column("role")] public UserRoleEnum Role { get; private set; }

    public Profile(int id, UserRoleEnum role)
    {
        Id = id;
        Role = role;
    }
}