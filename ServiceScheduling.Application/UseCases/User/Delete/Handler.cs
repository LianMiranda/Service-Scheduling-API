using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.Delete;

public sealed class Handler(IUserRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var userExists = await repository.GetByIdAsync(request.UserId, cancellationToken);
        
        if(userExists == null) return Result.Failure<Response>(new Error("404", "User not found"));
        
        await repository.DeleteAsync(userExists, cancellationToken);
        return Result.Success(new Response());
    }
}