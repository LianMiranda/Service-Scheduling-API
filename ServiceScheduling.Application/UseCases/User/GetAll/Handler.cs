using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.GetAll;

public sealed class Handler(IUserRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync(request.Skip, request.Take,cancellationToken: cancellationToken);
        
        if(users == null || users.Count == 0) return Result.Failure<Response>(new Error("404", "Users not found"));

        var userView = users.Select(user => user.ToDto()).ToList();
        
        return Result.Success(new Response(userView));
    }
}