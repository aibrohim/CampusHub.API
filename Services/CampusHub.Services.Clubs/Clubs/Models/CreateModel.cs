using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;

namespace CampusHub.Services.Clubs;

public class CreateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class CreateModelProfile: Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Club>();
    }
}

public class CreateClubModelValidator : AbstractValidator<CreateModel>
{
    public CreateClubModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Club name is required!")
            .MinimumLength(1).WithMessage("Club name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Club name must contain at most 100 charecter!");
    }
}