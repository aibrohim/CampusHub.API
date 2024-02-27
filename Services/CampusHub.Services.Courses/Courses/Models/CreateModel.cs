using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;

namespace CampusHub.Services.Courses;

public class CreateModel
{
	public string Name { get; set; }
}

public class CreateModelProfile: Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Course>();
    }
}

public class CreateCourseModelValidator : AbstractValidator<CreateModel>
{
    public CreateCourseModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Course name is required!")
            .MinimumLength(1).WithMessage("Course name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Course name must contain at most 100 charecter!");
    }
}