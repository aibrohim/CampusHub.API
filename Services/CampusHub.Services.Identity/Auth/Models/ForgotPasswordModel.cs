using FluentValidation;

namespace CampusHub.Services.Identity;

public class ForgotPasswordModel
{
    public string Email { get; set; }
}

public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
{
    public ForgotPasswordModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Invalid email!")
            .MinimumLength(1).WithMessage("Email must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Email must contain at most 100 charecter!");
    }
}