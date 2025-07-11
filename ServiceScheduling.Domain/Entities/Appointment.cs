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

    public User Client { get; private set; } = null!;

    [ForeignKey("serviceId")]
    [Column("serviceId")]
    public Guid ServiceId { get; private set; }

    public Service Service { get; private set; } = null!;
    [Column("date")] public DateTime Date { get; private set; }
    [Column("status")] public AppointmentStatus Status { get; private set; }

    public Appointment()
    {
    }

    public Appointment( DateTime date, Guid clientId, Guid serviceId)
    {
        Id = Guid.NewGuid();
        ServiceId = serviceId;
        ClientId = clientId;
        Date = date;
        Status = AppointmentStatus.Scheduled;
    }
    
    public void UpdateDate(DateTime date)
    {
        Date = date;
    }
    
    public void UpdateStatus(AppointmentStatus status)
    {
        Status = status;
    }
    
    public void UpdateClientId(Guid clientId)
    {
        ClientId = clientId;
    }
    
    public void UpdateServiceId(Guid serviceId)
    {
        ServiceId = serviceId;
    }
}