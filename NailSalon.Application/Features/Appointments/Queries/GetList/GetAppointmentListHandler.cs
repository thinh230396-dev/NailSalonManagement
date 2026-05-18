using MediatR;
using NailSalon.Application.Features.Appointments.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Appointments.Queries.GetList;

public class GetAppointmentListHandler : IRequestHandler<GetAppointmentListQuery, List<AppointmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentListHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AppointmentDto>> Handle(GetAppointmentListQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _unitOfWork.Repository<Appointment>().GetAllAsync();

        return appointments.Select(x => new AppointmentDto
        {
            Id = x.Id,
            AppointmentTime = x.AppointmentTime,
            Status = x.Status,
            Notes = x.Notes,
            CustomerId = x.CustomerId,
            EmployeeId = x.EmployeeId
        }).ToList();
    }
}