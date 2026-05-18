namespace NailSalon.Application.Features.NailServices.DTOs;

public class CreateNailServiceDto
{
    public string ServiceName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int DurationInMinutes { get; set; }
}