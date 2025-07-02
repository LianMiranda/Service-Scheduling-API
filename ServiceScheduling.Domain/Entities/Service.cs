using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceScheduling.Domain.Entities;

[Table("service")]
public class Service
{
    [Key] [Column("id")] public Guid Id { get; private set; }
    [Column("name")] public string Name { get; private set; }
    [Column("description")] public string Description { get; private set; }
    [Column("price")] public double Price { get; private set; }
    [Column("imageUrl")] public string ImageUrl { get; private set; }

    [Column("providerId")]
    [ForeignKey("providerId")]
    public Guid ProviderId { get; private set; }

    public User Provider { get; private set; }
    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    public Service()
    {
    }

    public Service(string name, string description, double price, string imageUrl, User provider)
    {
        if (provider == null) throw new ArgumentNullException(nameof(provider));

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        ProviderId = provider.Id;
        Provider = provider;
    }

    public void AddAppointment(Appointment appointment)
    {
        Appointments.Add(appointment);
    }

    public void RemoveAppointment(Appointment appointment)
    {
        Appointments.Remove(appointment);
    }
}