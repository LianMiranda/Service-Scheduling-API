using MediatR;
using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Update;

public sealed record Command(Guid Id, UpdateServiceDto ServiceData) : IRequest<Result<Response>>;