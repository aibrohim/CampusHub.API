using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;
using CampusHub.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Groups;

public class CreateModel
{
	public string Name { get; set; }
    public Guid CourseId { get; set; }
}

public class CreateModelProfile: Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Group>()
            .ForMember(dest => dest.CourseId, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    public class CreateModelActions : IMappingAction<CreateModel, Group>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateModel source, Group destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var course = db.Courses.FirstOrDefault(x => x.Uid == source.CourseId);

            destination.CourseId = course.Id;
        }
    }
}

public class CreateGroupModelValidator : AbstractValidator<CreateModel>
{
    public CreateGroupModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Group name is required!")
            .MinimumLength(1).WithMessage("Group name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Group name must contain at most 100 charecter!");

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("Course is required!")
            .Must((courseId) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Courses.Any(m => m.Uid == courseId);
                return found;
            }).WithMessage("Course not found!");
    }
}