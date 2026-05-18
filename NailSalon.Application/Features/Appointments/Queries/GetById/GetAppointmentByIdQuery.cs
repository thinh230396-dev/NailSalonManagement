using MediatR;
using NailSalon.Application.Features.Appointments.DTOs;

namespace NailSalon.Application.Features.Appointments.Queries.GetById;

public class GetAppointmentByIdQuery : IRequest<AppointmentDto>
{
    public Guid Id { get; set; }

    public GetAppointmentByIdQuery(Guid id)
    {
        Id = id;
    }
}