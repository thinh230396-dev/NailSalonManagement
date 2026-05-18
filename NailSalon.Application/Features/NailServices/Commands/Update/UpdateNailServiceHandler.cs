using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.NailServices.Commands.Update;

public class UpdateNailServiceHandler : IRequestHandler<UpdateNailServiceCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNailServiceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateNailServiceCommand request, CancellationToken cancellationToken)
    {
        var nailService = await _unitOfWork.Repository<NailService>().GetByIdAsync(request.Id);

        if (nailService == null)
            throw new NotFoundException(nameof(NailService), request.Id);

        nailService.ServiceName = request.ServiceName;
        nailService.Price = request.Price;
        nailService.DurationInMinutes = request.DurationInMinutes;

        _unitOfWork.Repository<NailService>().Update(nailService);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}