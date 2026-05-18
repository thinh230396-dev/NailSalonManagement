using FluentValidation;

namespace NailSalon.Application.Features.NailServices.Commands.Create;

public class CreateNailServiceCommandValidator
    : AbstractValidator<CreateNailServiceCommand>
{
    public CreateNailServiceCommandValidator()
    {
        RuleFor(x => x.ServiceName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0);
    }
}