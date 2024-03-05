
using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;

namespace CampusHub.Services.Clubs;

public class UpdateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateModelProfile: Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Club>();
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Club first name is required!")
            .MinimumLength(1).WithMessage("Club first name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Club first name must contain at most 100 charecter!");
    }
}

