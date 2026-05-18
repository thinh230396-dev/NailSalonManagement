using MediatR;

namespace NailSalon.Application.Features.Employees.Commands.Update;

public class UpdateEmployeeCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;
}