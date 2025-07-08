using Amazon.S3;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Application.Interfaces;


namespace ServiceScheduling.API.Controller.Service;

[ApiController]
[Route("api/v1/service")]
public class ServiceController : ControllerBase
{
    private readonly IAwsS3Service _s3Service;

    public ServiceController(IAwsS3Service s3Service)
    {
        _s3Service = s3Service;
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync(ISender sender, [FromForm] CreateServiceWithFileDto service,
        CancellationToken cancellationToken)
    {
        var key = string.Empty;
        var file = service.File;

        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (file != null && file.Length != 0)
            {
                await using var stream = file.OpenReadStream();
                key = service.Name + "-" + Guid.NewGuid();
                await _s3Service.UploadFileAsync(stream, key, file.ContentType, cancellationToken);
            }

            var command = new ServiceScheduling.Application.UseCases.Service.Save.Command(service, key);
            var result = await sender.Send(command, cancellationToken);

            if (result.isFailure)
            {
                if (result.Error.Code.Equals("401")) return Unauthorized(result.Error.Message);

                return BadRequest(result.Error.Message);
            }

            return StatusCode(201);
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

    [HttpGet]
    [Route("img/{serviceId}")]
    public async Task<IActionResult> GetServiceImageAsync(ISender sender, Guid serviceId,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new ServiceScheduling.Application.UseCases.Service.GetById.Command(serviceId);
            var service = await sender.Send(command, cancellationToken);

            if (service.isFailure) return NotFound(service.Error.Message);

            var key = service.Value.Service.ImageUrl;

            if (string.IsNullOrEmpty(key)) return NotFound("Image not registered");

            var imageStream = await _s3Service.DownloadFileAsync(key, cancellationToken);

            if (imageStream.Length == 0) return NotFound("Image not found");

            return File(imageStream, "image/jpeg");
        }
        catch (AmazonS3Exception e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
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