using MediatR;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.GetAll;

public sealed record Command(int Skip = 0, int Take = 10) : IRequest<Result<Response>>;