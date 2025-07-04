using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceScheduling.Application.DTOs.Service;

namespace ServiceScheduling.API.Controller.Service;

[ApiController]
[Route("api/v1/service")]
public class ServiceController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SaveAsync(ISender sender, [FromBody] CreateServiceDto service,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.Service.Save.Command(service);
            var result = await sender.Send(command, cancellationToken);

            if (result.isFailure)
            {
                if (result.Error.Code.Equals("401")) return Unauthorized(result.Error.Message);

                return BadRequest(result.Error.Message);
            }

            return Created();
        }
        catch (InvalidOperationException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(ISender sender,
        CancellationToken cancellationToken, [FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.Service.GetAll.Command(skip, take);
            var result = await sender.Send(command, cancellationToken);

            if (result.isFailure) return NotFound(result.Error.Message);
            return Ok(result.Value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    [HttpGet]
    [Route("{serviceId}")]
    public async Task<IActionResult> GetByIdAsync(ISender sender, Guid serviceId, CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.Service.GetById.Command(serviceId);
            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    [Route("{serviceId}")]
    public async Task<IActionResult> UpdateAsync(ISender sender, Guid serviceId,
        CancellationToken cancellationToken, [FromBody] UpdateServiceDto serviceData)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.Service.Update.Command(serviceId, serviceData);
            var result = await sender.Send(command, cancellationToken);

            if (result.isFailure)
            {
                return NotFound(result.Error.Message);
            }

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete]
    [Route("{serviceId}")]
    public async Task<IActionResult> DeleteAsync(ISender sender, Guid serviceId, CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.Service.Delete.Command(serviceId);
            var result = await sender.Send(command, cancellationToken);

            if (result.isFailure)
            {
                return NotFound(result.Error.Message);
            }

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}