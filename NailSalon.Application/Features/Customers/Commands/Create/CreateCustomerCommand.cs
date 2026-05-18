using MediatR;

namespace NailSalon.Application.Features.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<Guid>
{
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? Email { get; set; }
}