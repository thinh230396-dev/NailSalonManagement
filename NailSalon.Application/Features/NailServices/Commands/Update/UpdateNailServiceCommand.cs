using MediatR;

namespace NailSalon.Application.Features.NailServices.Commands.Update;

public class UpdateNailServiceCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string ServiceName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int DurationInMinutes { get; set; }
}