using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;
using CampusHub.Settings;
using CampusHub.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.ClubMeetings;

public class CreateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }

    public Guid ClubId { get; set; }
    public Guid RoomId { get; set; }
}

public class CreateModelProfile: Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, ClubMeeting>()
            .ForMember(dest => dest.RoomId, opt => opt.Ignore())
            .ForMember(dest => dest.ClubId, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    public class CreateModelActions : IMappingAction<CreateModel, ClubMeeting>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateModel source, ClubMeeting destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var club = db.Clubs.FirstOrDefault(x => x.Uid == source.ClubId);
            var room = db.Rooms.FirstOrDefault(x => x.Uid == source.RoomId);

            destination.ClubId = club.Id;
            destination.RoomId = room.Id;
        }
    }
}

public class CreateClubMeetingModelValidator : AbstractValidator<CreateModel>
{
    public CreateClubMeetingModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Club Meeting title is required!")
            .MinimumLength(1).WithMessage("Club Meeting title must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Club Meeting title must contain at most 100 charecter!");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Club Meeting description is required!");

        RuleFor(x => x.DateTime)
            .NotEmpty().WithMessage("Club Meeting date time is required!")
            .NotNull().WithMessage("Club Meeting date time is required!")
            .BeTodayOrLater("Club Meeting date must be today or in the future!");

        RuleFor(x => x.ClubId)
            .NotEmpty().WithMessage("Club is required")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Clubs.Any(m => m.Uid == id);
                return found;
            }).WithMessage("Club not found!");

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