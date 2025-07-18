using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceScheduling.Domain.Enums;

namespace ServiceScheduling.Application.DTOs.Appointment;

public class CreateAppointmentDto
{
    [Required] public DateTime Date { get; set; }
    [Required] public Guid ClientId { get; set; }
    [Required] public Guid ServiceId { get; set; }

    public CreateAppointmentDto()
    {
    }

    public CreateAppointmentDto(DateTime date, AppointmentStatus status, Guid clientId, Guid serviceId)
    {
        Date = date;
        ClientId = clientId;
        ServiceId = serviceId;
    }
}