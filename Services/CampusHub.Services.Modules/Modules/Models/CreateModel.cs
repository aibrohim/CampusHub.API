using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;
using CampusHub.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Modules;

public class CreateModel
{
	public string Name { get; set; }
    public Guid CourseId { get; set; }
}

public class CreateModelProfile: Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Module>()
            .ForMember(dest => dest.CourseId, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    public class CreateModelActions : IMappingAction<CreateModel, Module>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateModel source, Module destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var course = db.Courses.FirstOrDefault(x => x.Uid == source.CourseId);

            destination.CourseId = course.Id;
        }
    }
}

public class CreateRoomModelValidator : AbstractValidator<CreateModel>
{
    public CreateRoomModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Module name is required!")
            .MinimumLength(1).WithMessage("Module name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Module name must contain at most 100 charecter!");

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("Course is required")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Courses.Any(b => b.Uid == id);
                return found;
            }).WithMessage("Course not found");

    }
}