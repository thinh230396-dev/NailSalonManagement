using MediatR;
using NailSalon.Application.Features.NailServices.DTOs;

namespace NailSalon.Application.Features.NailServices.Queries.GetList;

public class GetNailServiceListQuery : IRequest<List<NailServiceDto>>
{
}