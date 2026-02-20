namespace PestControlCMS.Application.Validators;

/// <summary>
/// FluentValidation rules for <see cref="CreateContractRequest"/>.
/// </summary>
public sealed class CreateContractValidator : AbstractValidator<CreateContractRequest>
{
    /// <summary>Initializes all validation rules.</summary>
    public CreateContractValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(200).WithMessage("Customer name cannot exceed 200 characters.");

        RuleFor(x => x.PropertyAddress)
            .NotEmpty().WithMessage("Property address is required.")
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Start date cannot be in the past.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after the start date.");

        RuleFor(x => x.AnnualFee)
            .GreaterThan(0).WithMessage("Annual fee must be greater than zero.")
            .LessThanOrEqualTo(1_000_000).WithMessage("Annual fee seems unreasonably high.");

        RuleFor(x => x.ServiceFrequency)
            .IsInEnum().WithMessage("Invalid service frequency.");

        RuleFor(x => x.PestTypes)
            .Must(p => p != PestType.None)
            .WithMessage("At least one pest type must be selected.");

        RuleFor(x => x.SpecialInstructions)
            .MaximumLength(1000)
            .WithMessage("Special instructions cannot exceed 1000 characters.")
            .When(x => x.SpecialInstructions is not null);
    }
}
