using MediatR;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.GetById;

public sealed record Command(Guid Id) : IRequest<Result<Response>>;