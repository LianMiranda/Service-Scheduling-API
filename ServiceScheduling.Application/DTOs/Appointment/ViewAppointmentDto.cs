using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Application.DTOs.User;
using ServiceScheduling.Domain.Enums;

namespace ServiceScheduling.Application.DTOs.Appointment;

public class ViewAppointmentDto
{
    public DateTime Date { get; set; }
    public AppointmentStatus Status { get; set; }
    public ViewUserDto Client { get; set; }
    public ViewServiceDto Service { get; set; }
}