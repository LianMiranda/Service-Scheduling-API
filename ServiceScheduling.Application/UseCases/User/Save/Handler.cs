using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.Save;

public sealed class Handler(IUserRepository repository, IPasswordHasher passwordHasher) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var properties = request.User.GetType().GetProperties();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(request.User);

            if (value == null)
                return Result.Failure<Response>(new Error("400", $"Argument {prop.Name} is null"));


            if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value.ToString()))
                return Result.Failure<Response>(new Error("400", $"Argument {prop.Name} is null"));;
        }
        
        string passwordHash = passwordHasher.Hash(request.User.Password);

        request.User.Password = passwordHash;
        
        var entity = request.User.ToEntity();
        await repository.SaveAsync(entity, cancellationToken);

        return Result.Success(new Response());
    }
}