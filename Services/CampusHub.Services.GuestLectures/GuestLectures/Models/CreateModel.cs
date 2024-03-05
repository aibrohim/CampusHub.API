using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;
using CampusHub.Settings;
using CampusHub.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.GuestLectures;

public class CreateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }

    public Guid GuestId { get; set; }
    public Guid RoomId { get; set; }
}

public class CreateModelProfile: Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, GuestLecture>()
            .ForMember(dest => dest.RoomId, opt => opt.Ignore())
            .ForMember(dest => dest.GuestId, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    public class CreateModelActions : IMappingAction<CreateModel, GuestLecture>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateModel source, GuestLecture destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var guest = db.Guests.FirstOrDefault(x => x.Uid == source.GuestId);
            var room = db.Rooms.FirstOrDefault(x => x.Uid == source.RoomId);

            destination.GuestId = guest.Id;
            destination.RoomId = room.Id;
        }
    }
}

public class CreateGuestLectureModelValidator : AbstractValidator<CreateModel>
{
    public CreateGuestLectureModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Guest Lecture title is required!")
            .MinimumLength(1).WithMessage("Guest Lecture title must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Guest Lecture title must contain at most 100 charecter!");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Guest Lecture description is required!");

        RuleFor(x => x.DateTime)
            .NotEmpty().WithMessage("Guest Lecture date time is required!")
            .NotNull().WithMessage("Guest Lecture date time is required!")
            .BeTodayOrLater("Guest Lecture date must be today or in the future!");

        RuleFor(x => x.GuestId)
            .NotEmpty().WithMessage("Guest is required")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Guests.Any(m => m.Uid == id);
                return found;
            }).WithMessage("Guest not found!");

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