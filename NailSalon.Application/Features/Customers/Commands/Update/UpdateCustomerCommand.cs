using MediatR;

namespace NailSalon.Application.Features.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? Email { get; set; }

    public int LoyaltyPoints { get; set; }
}