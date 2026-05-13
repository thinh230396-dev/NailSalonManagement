using AutoMapper;
using NailSalon.Application.DTOs.NailService;
using NailSalon.Application.Interfaces;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Interfaces.Repositories;

namespace NailSalon.Application.Services;

public class NailServiceLogic : INailServiceLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NailServiceLogic(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NailServiceDto>> GetAllServicesAsync()
    {
        var services = await _unitOfWork.Repository<NailService>().GetAllAsync();
        return _mapper.Map<IEnumerable<NailServiceDto>>(services);
    }

    public async Task<NailServiceDto?> GetServiceByIdAsync(Guid id)
    {
        var service = await _unitOfWork.Repository<NailService>().GetByIdAsync(id);
        return _mapper.Map<NailServiceDto>(service);
    }

    public async Task<NailServiceDto> CreateServiceAsync(CreateUpdateNailServiceDto dto)
    {
        var service = _mapper.Map<NailService>(dto);

        await _unitOfWork.Repository<NailService>().AddAsync(service);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<NailServiceDto>(service);
    }

    public async Task UpdateServiceAsync(Guid id, CreateUpdateNailServiceDto dto)
    {
        var service = await _unitOfWork.Repository<NailService>().GetByIdAsync(id);
        if (service == null) throw new Exception("Không tìm thấy dịch vụ.");

        _mapper.Map(dto, service);

        _unitOfWork.Repository<NailService>().Update(service);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteServiceAsync(Guid id)
    {
        var service = await _unitOfWork.Repository<NailService>().GetByIdAsync(id);
        if (service == null) throw new Exception("Không tìm thấy dịch vụ.");

        _unitOfWork.Repository<NailService>().Delete(service);
        await _unitOfWork.SaveChangesAsync();
    }
}