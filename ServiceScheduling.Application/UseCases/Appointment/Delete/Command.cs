using MediatR;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Appointment.Delete;

public sealed record Command(Guid AppointmentId) : IRequest<Result<Response>>;