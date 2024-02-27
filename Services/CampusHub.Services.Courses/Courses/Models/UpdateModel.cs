
using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;

namespace CampusHub.Services.Courses;

public class UpdateModel
{
    public string Name { get; set; }
}

public class UpdateModelProfile: Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Course>();
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Course name is required!")
            .MinimumLength(1).WithMessage("Course name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Course name must contain at most 100 charecter!");
    }
}

