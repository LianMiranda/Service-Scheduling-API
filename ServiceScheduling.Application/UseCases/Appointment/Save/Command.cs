using MediatR;
using ServiceScheduling.Application.DTOs.Appointment;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Appointment.Save;

public sealed record Command(CreateAppointmentDto Appointment) : IRequest<Result<Response>>;