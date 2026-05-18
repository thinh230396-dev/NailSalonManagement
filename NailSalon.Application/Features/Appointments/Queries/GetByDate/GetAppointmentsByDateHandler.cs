using MediatR;
using NailSalon.Application.Features.Appointments.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Appointments.Queries.GetByDate;

public class GetAppointmentsByDateHandler : IRequestHandler<GetAppointmentsByDateQuery, List<AppointmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentsByDateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AppointmentDto>> Handle(GetAppointmentsByDateQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _unitOfWork.Repository<Appointment>().GetAllAsync();

        return appointments
            .Where(x => x.AppointmentTime.Date == request.Date.Date)
            .Select(x => new AppointmentDto
            {
                Id = x.Id,
                AppointmentTime = x.AppointmentTime,
                Status = x.Status,
                Notes = x.Notes,
                CustomerId = x.CustomerId,
                EmployeeId = x.EmployeeId
            })
            .ToList();
    }
}