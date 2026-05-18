using MediatR;

namespace NailSalon.Application.Features.Employees.Commands.Delete;

public class DeleteEmployeeCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public DeleteEmployeeCommand(Guid id)
    {
        Id = id;
    }
}