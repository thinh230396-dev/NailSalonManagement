using MediatR;
using NailSalon.Application.Features.Appointments.DTOs;

namespace NailSalon.Application.Features.Appointments.Queries.GetByDate;

public class GetAppointmentsByDateQuery : IRequest<List<AppointmentDto>>
{
    public DateTime Date { get; set; }

    public GetAppointmentsByDateQuery(DateTime date)
    {
        Date = date;
    }
}