using System.Text.Json.Serialization;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Application.DTOs.User;
using ServiceScheduling.Domain.Enums;

namespace ServiceScheduling.Application.DTOs.Appointment;

public class ViewAppointmentDto
{
    [JsonPropertyOrder(0)] public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public AppointmentStatus Status { get; set; }
    public ViewLimitedUserDto Client { get; set; }
    public ViewLimitedServiceDto Service { get; set; }
}