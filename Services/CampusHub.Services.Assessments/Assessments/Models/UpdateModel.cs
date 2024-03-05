using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;
using CampusHub.Context;
using Microsoft.EntityFrameworkCore;
using CampusHub.Settings;

namespace CampusHub.Services.Assessments;

public class UpdateModel
{
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid ModuleId { get; set; }
    public Guid RoomId { get; set; }
}

public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Assessment>()
            .ForMember(dest => dest.RoomId, opt => opt.Ignore())
            .ForMember(dest => dest.ModuleId, opt => opt.Ignore())
            .AfterMap<UpdateModelActions>();
    }

    public class UpdateModelActions : IMappingAction<UpdateModel, Assessment>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public UpdateModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(UpdateModel source, Assessment destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var module = db.Modules.FirstOrDefault(x => x.Uid == source.ModuleId);
            var room = db.Rooms.FirstOrDefault(x => x.Uid == source.RoomId);

            destination.ModuleId = module.Id;
            destination.RoomId = room.Id;
        }
    }
}

public class UpdateAssessmentModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateAssessmentModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Assessment title is required!")
            .MinimumLength(1).WithMessage("Assessment title must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Assessment title must contain at most 100 charecter!");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start Date is required")
            .NotNull().WithMessage("Start Date is required")
            .BeTodayOrLater("Start Date must be today or in the future.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End Date is required")
            .NotNull().WithMessage("End Date is required")
            .BeTodayOrLater("End Date must be today or in the future.");

        RuleFor(x => x.ModuleId)
            .NotEmpty().WithMessage("Module is required")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Modules.Any(m => m.Uid == id);
                return found;
            }).WithMessage("Module not found!");

        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("Room is required")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Rooms.Any(r => r.Uid == id);
                return found;
            }).WithMessage("Room not found!");
    }

}