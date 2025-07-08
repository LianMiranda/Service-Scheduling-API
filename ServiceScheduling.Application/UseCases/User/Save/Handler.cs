using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.Save;

public sealed class Handler(IUserRepository repository, IPasswordHasher passwordHasher) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        string passwordHash = passwordHasher.Hash(request.User.Password);

        request.User.Password = passwordHash;
        
        var entity = request.User.ToEntity();
        await repository.SaveAsync(entity, cancellationToken);

        return Result.Success(new Response());
    }
}