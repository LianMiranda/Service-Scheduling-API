using System.Reflection.Metadata;
using MediatR;
using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.Update;

public sealed class Handler(IUserRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.Id);

        if (user == null) return Result.Failure<Response>(new Error("404", "User not found"));
        
        if(!string.IsNullOrWhiteSpace(request.UserData.Name)) user.UpdateName(request.UserData.Name);
        if(!string.IsNullOrWhiteSpace(request.UserData.Email)) user.UpdateEmail(request.UserData.Email);
        if(!string.IsNullOrWhiteSpace(request.UserData.Password)) user.UpdatePassword(request.UserData.Password);

        await repository.UpdateAsync(user, cancellationToken);
        
        return Result.Success(new Response());
    }
}