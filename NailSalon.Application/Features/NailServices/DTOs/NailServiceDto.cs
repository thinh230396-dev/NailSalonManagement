namespace NailSalon.Application.Features.NailServices.DTOs;

public class NailServiceDto
{
    public Guid Id { get; set; }

    public string ServiceName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int DurationInMinutes { get; set; }
}