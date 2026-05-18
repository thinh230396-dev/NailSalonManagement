using MediatR;

namespace NailSalon.Application.Features.Employees.Commands.Create;

public class CreateEmployeeCommand : IRequest<Guid>
{
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;
}