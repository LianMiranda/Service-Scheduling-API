using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Domain.Entities;
using ServiceScheduling.Domain.Enums;

namespace ServiceScheduling.Infra.Data;

public class ContextDatabase : DbContext
{
    public ContextDatabase(DbContextOptions options) : base(options)
    {
    }
    
    DbSet<Service> Services { get; set; }
    DbSet<Appointment> Appointments { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>().HasData(
            new Profile(1, UserRoleEnum.Client),
            new Profile(2, UserRoleEnum.Admin)
        );
    }
}