using AutoMapper;
using NailSalon.Application.DTOs.Appointment;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName));

        CreateMap<CreateUpdateAppointmentDto, Appointment>();
    }
}