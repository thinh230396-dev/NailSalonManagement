using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Features.NailServices.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.NailServices.Queries.GetById;

public class GetNailServiceByIdHandler : IRequestHandler<GetNailServiceByIdQuery, NailServiceDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNailServiceByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<NailServiceDto> Handle(GetNailServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var nailService = await _unitOfWork.Repository<NailService>().GetByIdAsync(request.Id);

        if (nailService == null)
            throw new NotFoundException(nameof(NailService), request.Id);

        return new NailServiceDto
        {
            Id = nailService.Id,
            ServiceName = nailService.ServiceName,
            Price = nailService.Price,
            DurationInMinutes = nailService.DurationInMinutes
        };
    }
}