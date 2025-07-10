using ServiceScheduling.Application.DTOs.Appointment;

namespace ServiceScheduling.Application.UseCases.Appointment.GetById;

public sealed record Response(ViewAppointmentDto Appointment);