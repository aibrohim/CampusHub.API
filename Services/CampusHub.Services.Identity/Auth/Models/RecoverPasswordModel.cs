using FluentValidation;

namespace CampusHub.Services.Identity;

public class RecoverPasswordModel
{
    public string Email { get; set; }
    public string RecoveryToken { get; set; }
    public string NewPassword { get; set; }
}

public class RecoverPasswordModelModelValidator : AbstractValidator<RecoverPasswordModel>
{
    public RecoverPasswordModelModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Invalid email!")
            .MinimumLength(1).WithMessage("Email must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Email must contain at most 100 charecter!");

        RuleFor(x => x.RecoveryToken)
            .NotEmpty().WithMessage("Recovery token is required!");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New Password is required!")
            .MinimumLength(4).WithMessage("New Password must contain at least 4 charecters!")
            .MaximumLength(50).WithMessage("New Password must contain at most 100 charecters!");
    }
}