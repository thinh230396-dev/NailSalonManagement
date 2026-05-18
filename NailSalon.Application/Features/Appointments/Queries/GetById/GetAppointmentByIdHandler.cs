using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Features.Appointments.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Appointments.Queries.GetById;

public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(request.Id);

        if (appointment == null)
            throw new NotFoundException(nameof(Appointment), request.Id);

        return new AppointmentDto
        {
            Id = appointment.Id,
            AppointmentTime = appointment.AppointmentTime,
            Status = appointment.Status,
            Notes = appointment.Notes,
            CustomerId = appointment.CustomerId,
            EmployeeId = appointment.EmployeeId
        };
    }
}