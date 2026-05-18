using MediatR;

namespace NailSalon.Application.Features.NailServices.Commands.Delete;

public class DeleteNailServiceCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public DeleteNailServiceCommand(Guid id)
    {
        Id = id;
    }
}