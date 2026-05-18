using MediatR;
using NailSalon.Application.Features.NailServices.DTOs;

namespace NailSalon.Application.Features.NailServices.Queries.GetById;

public class GetNailServiceByIdQuery : IRequest<NailServiceDto>
{
    public Guid Id { get; set; }

    public GetNailServiceByIdQuery(Guid id)
    {
        Id = id;
    }
}