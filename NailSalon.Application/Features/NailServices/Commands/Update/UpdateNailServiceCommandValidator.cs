using FluentValidation;

namespace NailSalon.Application.Features.NailServices.Commands.Update;

public class UpdateNailServiceCommandValidator : AbstractValidator<UpdateNailServiceCommand>
{
    public UpdateNailServiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.ServiceName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0);
    }
}