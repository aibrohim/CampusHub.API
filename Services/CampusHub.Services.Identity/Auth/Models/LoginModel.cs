using System;
using FluentValidation;

namespace CampusHub.Services.Identity;

public class LoginModel
{
	public string Email { get; set; }
	public string Password { get; set; }
}

public class LoginModelModelValidator : AbstractValidator<LoginModel>
{
	public LoginModelModelValidator()
	{
		RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required!")
				.EmailAddress().WithMessage("Invalid email!")
				.MinimumLength(1).WithMessage("Email must contain at least 1 charecter!")
				.MaximumLength(100).WithMessage("Email must contain at most 100 charecter!");

		RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required!")
				.MinimumLength(4).WithMessage("Password must contain at least 4 charecters!")
				.MaximumLength(50).WithMessage("Password must contain at most 100 charecters!");
	}
}