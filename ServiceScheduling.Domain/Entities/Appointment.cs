using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceScheduling.Domain.Enums;

namespace ServiceScheduling.Domain.Entities;

[Table("appointment")]
public class Appointment
{
    [Key] [Column("id")] public Guid Id { get; private set; }

    [ForeignKey("clientId")]
    [Column("clientId")]
    public Guid ClientId { get; private set; }

    public User Client { get; private set; }

    [ForeignKey("serviceId")]
    [Column("serviceId")]
    public Guid ServiceId { get; private set; }

    public Service Service { get; private set; }
    [Column("date")] public DateTime Date { get; private set; }
    [Column("status")] public AppointmentStatus Status { get; private set; }

    public Appointment()
    {
    }

    public Appointment(User client, Service service, DateTime date, AppointmentStatus status)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        Id = Guid.NewGuid();
        ServiceId = service.Id;
        Service = service;
        Client = client;
        ClientId = client.Id;
        Date = date;
        Status = status;
    }
}