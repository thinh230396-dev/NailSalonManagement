using AutoMapper;
using NailSalon.Application.DTOs.NailService;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Mappings;

public class NailServiceProfile : Profile
{
    public NailServiceProfile()
    {
        CreateMap<NailService, NailServiceDto>();
        CreateMap<CreateUpdateNailServiceDto, NailService>();
    }
}