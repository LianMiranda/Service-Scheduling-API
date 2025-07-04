using MediatR;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Delete;

public sealed record Command(Guid serviceId) : IRequest<Result<Response>>;