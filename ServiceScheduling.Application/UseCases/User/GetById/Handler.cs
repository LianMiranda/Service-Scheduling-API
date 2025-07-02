using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.GetById;

public sealed class Handler(IUserRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.Id, cancellationToken);

        return user is null
            ? Result.Failure<Response>(new Error("404", "User not found"))
            : Result.Success(new Response(user.ToDto()));
    }
}