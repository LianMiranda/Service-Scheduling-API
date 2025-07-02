using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Domain.Entities;
using ServiceScheduling.Domain.Enums;

namespace ServiceScheduling.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    DbSet<Service> Services { get; set; }
    DbSet<Appointment> Appointments { get; set; }
    DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Profile>().HasData(
            new Profile(1, UserRoleEnum.Client),
            new Profile(2, UserRoleEnum.Admin)
        );
    }
}