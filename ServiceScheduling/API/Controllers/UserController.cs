using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Common.Exceptions;

namespace ServiceScheduling.API.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync([FromBody] CreateUserDto user, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.SaveAsync(user, cancellationToken);
            return Created();
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var users = await _userService.GetAllAsync(skip, take, cancellationToken);
            return Ok(users);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _userService.GetByIdAsync(userId, cancellationToken);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    [Route("{userId}")]
    public async Task<IActionResult> UpdateAsync(Guid userId, UpdateUserDto user, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.UpdateAsync(userId, user, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        try
        {
            await _userService.DeleteAsync(userId, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}