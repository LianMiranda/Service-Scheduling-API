using Microsoft.AspNetCore.Mvc;
using MediatR;
using ServiceScheduling.Application.DTOs.Appointment;
using ServiceScheduling.Application.DTOs.Service;

namespace ServiceScheduling.API.Controller.Appointment;




[ApiController]
[Route("api/v1/appointments")]
public class AppointmentController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> SaveAsync(ISender sender, [FromForm] CreateAppointmentDto appointment,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new ServiceScheduling.Application.UseCases.Appointment.Save.Command(appointment);
            var result = await sender.Send(command, cancellationToken);

            if (result.isFailure)
            {
                return BadRequest(result.Error.Message);
            }

            return StatusCode(201);
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
            var command = new ServiceScheduling.Application.UseCases.Appointment.GetAll.Command(skip, take);
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
    [Route("{appointmentId}")]
    public async Task<IActionResult> GetByIdAsync(ISender sender, Guid appointmentId, CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.Appointment.GetById.Command(appointmentId);
            var result = await sender.Send(command, cancellationToken);

            if(result.isFailure)
            {
                if(result.Error.Code.Equals("400")) return BadRequest(result.Error.Message);
                
                return NotFound(result.Error.Message);
            }
            
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    [HttpPut]
    [Route("{appointmentId}")]
    public async Task<IActionResult> UpdateAsync(ISender sender, Guid appointmentId,
        CancellationToken cancellationToken, [FromForm] UpdateAppointmentDto appointmentData)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.Appointment.Update.Command(appointmentId, appointmentData);
            
            var result = await sender.Send(command, cancellationToken);

            if(result.isFailure)
            {
                if(result.Error.Code.Equals("400")) return BadRequest(result.Error.Message);
                
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
    [Route("{appointmentId}")]
    public async Task<IActionResult> DeleteAsync(ISender sender, Guid appointmentId, CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.Appointment.Delete.Command(appointmentId);
            var result = await sender.Send(command, cancellationToken);

            if(result.isFailure)
            {
                if(result.Error.Code.Equals("400")) return BadRequest(result.Error.Message);
                
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