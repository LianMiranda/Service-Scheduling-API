using ServiceScheduling.Application.DTOs.Appointment;

namespace ServiceScheduling.Application.UseCases.Appointment.GetAll;

public sealed record Response(List<ViewAppointmentDto> Appointments);