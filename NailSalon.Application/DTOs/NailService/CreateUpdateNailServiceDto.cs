namespace NailSalon.Application.DTOs.NailService;

public class CreateUpdateNailServiceDto
{
    public string ServiceName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationInMinutes { get; set; }
}