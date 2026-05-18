using FluentValidation;

namespace NailSalon.Application.Features.Invoices.Commands.AddService;

public class AddServiceToInvoiceCommandValidator : AbstractValidator<AddServiceToInvoiceCommand>
{
    public AddServiceToInvoiceCommandValidator()
    {
        RuleFor(x => x.InvoiceId)
            .NotEmpty();

        RuleFor(x => x.NailServiceId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}