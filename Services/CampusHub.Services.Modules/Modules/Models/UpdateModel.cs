using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;
using CampusHub.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Modules;

public class UpdateModel
{
    public string Name { get; set; }
    public Guid CourseId { get; set; }
}

public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Module>()
            .ForMember(dest => dest.CourseId, opt => opt.Ignore())
            .AfterMap<UpdateModelActions>();
    }

    public class UpdateModelActions : IMappingAction<UpdateModel, Module>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public UpdateModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(UpdateModel source, Module destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var course = db.Courses.FirstOrDefault(x => x.Uid == source.CourseId);

            destination.CourseId = course.Id;
        }
    }
}

public class UpdateRoomModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateRoomModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Module Name must be specified!")
            .MinimumLength(1).WithMessage("Module name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Module name must contain at most 100 charecter!");

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("Course Id must be specified!")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Courses.Any(c => c.Uid == id);
                return found;
            }).WithMessage("Course not found");

    }
}