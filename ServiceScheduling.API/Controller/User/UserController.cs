using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceScheduling.Application.DTOs.User;

namespace ServiceScheduling.API.Controller.User;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SaveAsync(ISender sender, [FromBody] CreateUserDto user,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.User.Save.Command(user);
            var result = await sender.Send(command, cancellationToken);

            if (result.isFailure) return BadRequest(result.Error.Message);

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
            var command = new ServiceScheduling.Application.UseCases.User.GetAll.Command(skip, take);
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
    [Route("{userId}")]
    public async Task<IActionResult> GetByIdAsync(ISender sender, Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.User.GetById.Command(userId);
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
    [Route("{userId}")]
    public async Task<IActionResult> UpdateAsync(ISender sender, Guid userId, UpdateUserDto userData,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.User.Update.Command(userId, userData);
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
    public async Task<IActionResult> DeleteAsync(ISender sender, Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.User.Delete.Command(userId);
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