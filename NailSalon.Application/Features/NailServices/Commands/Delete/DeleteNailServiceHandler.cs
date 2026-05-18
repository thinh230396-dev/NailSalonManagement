using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.NailServices.Commands.Delete;

public class DeleteNailServiceHandler : IRequestHandler<DeleteNailServiceCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNailServiceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteNailServiceCommand request, CancellationToken cancellationToken)
    {
        var nailService = await _unitOfWork.Repository<NailService>().GetByIdAsync(request.Id);

        if (nailService == null)
            throw new NotFoundException(nameof(NailService), request.Id);

        _unitOfWork.Repository<NailService>().Delete(nailService);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}