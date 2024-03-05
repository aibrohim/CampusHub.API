using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;

namespace CampusHub.Services.Guests;

public class CreateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
}

public class CreateModelProfile: Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Guest>();
    }
}

public class CreateGuestModelValidator : AbstractValidator<CreateModel>
{
    public CreateGuestModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Guest first name is required!")
            .MinimumLength(1).WithMessage("Guest first name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Guest first name must contain at most 100 charecter!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Guest last name is required!")
            .MinimumLength(1).WithMessage("Guest last name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Guest last name must contain at most 100 charecter!");
    }
}