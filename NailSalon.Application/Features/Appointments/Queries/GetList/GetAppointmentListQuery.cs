using MediatR;
using NailSalon.Application.Features.Appointments.DTOs;

namespace NailSalon.Application.Features.Appointments.Queries.GetList;

public class GetAppointmentListQuery : IRequest<List<AppointmentDto>>
{
}