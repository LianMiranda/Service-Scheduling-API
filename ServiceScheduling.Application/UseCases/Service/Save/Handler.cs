using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Save;

public class Handler(IServiceRepository serviceRepository, IUserRepository userRepository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var properties = request.Service.GetType().GetProperties();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(request.Service);

            if (value == null) return Result.Failure<Response>(new Error("400", $"Argument {prop.Name} is null"));
            
            if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value.ToString()))
                return Result.Failure<Response>(new Error("400", $"Argument {prop.Name} is null"));
        }
        
        var verifyUserRole = await userRepository.GetByIdAsync(request.Service.ProviderId);

        if (verifyUserRole != null && verifyUserRole.ProfileId == 2)
        {
            var entity = request.Service.ToEntity();
            await serviceRepository.SaveAsync(entity, cancellationToken);
        }
        else
        {
            return Result.Failure<Response>(new Error("401", "you are not authorized"));
        }
        
        //TODO: Salvar a foto na AWS S3.

        return verifyUserRole is null
            ? Result.Failure<Response>(new Error("400", $"Provider is null"))
            : Result.Success(new Response());
    }
}