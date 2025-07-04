using MediatR;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.GetById;

public sealed record Command(Guid Id) : IRequest<Result<Response>>;