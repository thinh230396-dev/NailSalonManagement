using MediatR;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.NailServices.Commands.Create;

public class CreateNailServiceHandler
    : IRequestHandler<CreateNailServiceCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateNailServiceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateNailServiceCommand request,
        CancellationToken cancellationToken)
    {
        var nailService = new NailService
        {
            Id = Guid.NewGuid(),
            ServiceName = request.ServiceName,
            Price = request.Price,
            DurationInMinutes = request.DurationInMinutes
        };

        await _unitOfWork
            .Repository<NailService>()
            .AddAsync(nailService);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return nailService.Id;
    }
}