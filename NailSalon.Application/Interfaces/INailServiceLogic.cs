using NailSalon.Application.DTOs.NailService;

namespace NailSalon.Application.Interfaces;

public interface INailServiceLogic
{
    Task<IEnumerable<NailServiceDto>> GetAllServicesAsync();
    Task<NailServiceDto?> GetServiceByIdAsync(Guid id);
    Task<NailServiceDto> CreateServiceAsync(CreateUpdateNailServiceDto dto);
    Task UpdateServiceAsync(Guid id, CreateUpdateNailServiceDto dto);
    Task DeleteServiceAsync(Guid id);
}