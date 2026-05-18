using MediatR;

namespace NailSalon.Application.Features.NailServices.Commands.Create;

public class CreateNailServiceCommand : IRequest<Guid>
{
    public string ServiceName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int DurationInMinutes { get; set; }
}