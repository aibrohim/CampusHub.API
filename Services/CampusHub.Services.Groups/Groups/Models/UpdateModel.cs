
using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;
using CampusHub.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Groups;

public class UpdateModel
{
    public string Name { get; set; }
    public Guid CourseId { get; set; }
}

public class UpdateModelProfile: Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Group>()
            .ForMember(dest => dest.CourseId, opt => opt.Ignore())
            .AfterMap<UpdateModelActions>();
    }

    public class UpdateModelActions : IMappingAction<UpdateModel, Group>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public UpdateModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(UpdateModel source, Group destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var course = db.Courses.FirstOrDefault(x => x.Uid == source.CourseId);

            destination.CourseId = course.Id;
        }
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator(IDbContextFactory<MainDbContext> contextFactory)
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

