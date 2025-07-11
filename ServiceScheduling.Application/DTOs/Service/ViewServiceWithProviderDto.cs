using System.Text.Json.Serialization;
using ServiceScheduling.Application.DTOs.User;

namespace ServiceScheduling.Application.DTOs.Service;

public class ViewServiceWithProviderDto : ViewServiceDto
{
    [JsonPropertyOrder(99)] public ViewProviderDto Provider { get; set; }
}