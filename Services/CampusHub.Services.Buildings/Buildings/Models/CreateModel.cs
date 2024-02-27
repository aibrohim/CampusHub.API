using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;

namespace CampusHub.Services.Buildings;

public class CreateModel
{
	public string Name { get; set; }
}

public class CreateModelProfile: Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Building>();
    }
}

public class CreateBuildingModelValidator : AbstractValidator<CreateModel>
{
    public CreateBuildingModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Building name is required!")
            .MinimumLength(1).WithMessage("Building name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Building name must contain at most 100 charecter!");
    }
}