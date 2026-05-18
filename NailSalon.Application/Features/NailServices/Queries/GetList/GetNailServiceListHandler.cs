using MediatR;
using NailSalon.Application.Features.NailServices.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.NailServices.Queries.GetList;

public class GetNailServiceListHandler
    : IRequestHandler<GetNailServiceListQuery, List<NailServiceDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNailServiceListHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<NailServiceDto>> Handle(
        GetNailServiceListQuery request,
        CancellationToken cancellationToken)
    {
        var services = await _unitOfWork
            .Repository<NailService>()
            .GetAllAsync();

        return services.Select(x => new NailServiceDto
        {
            Id = x.Id,
            ServiceName = x.ServiceName,
            Price = x.Price,
            DurationInMinutes = x.DurationInMinutes
        }).ToList();
    }
}