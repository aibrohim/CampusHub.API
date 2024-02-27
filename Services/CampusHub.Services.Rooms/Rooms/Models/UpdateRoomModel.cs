using AutoMapper;
using FluentValidation;

using CampusHub.Context.Entities;
using CampusHub.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Rooms;

public class UpdateRoomModel
{
    public string Name { get; set; }
    public Guid BuildingId { get; set; }
}

public class UpdateRoomModelProfile : Profile
{
    public UpdateRoomModelProfile()
    {
        CreateMap<UpdateRoomModel, Room>()
            .ForMember(dest => dest.BuildingId, opt => opt.Ignore())
            .AfterMap<UpdateRoomModelActions>();
    }

    public class UpdateRoomModelActions : IMappingAction<UpdateRoomModel, Room>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public UpdateRoomModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(UpdateRoomModel source, Room destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var building = db.Buildings.FirstOrDefault(x => x.Uid == source.BuildingId);

            destination.BuildingId = building.Id;
        }
    }
}

public class UpdateRoomModelValidator : AbstractValidator<UpdateRoomModel>
{
    public UpdateRoomModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Building name is required!")
            .MinimumLength(1).WithMessage("Building name must contain at least 1 charecter!")
            .MaximumLength(100).WithMessage("Building name must contain at most 100 charecter!");

        RuleFor(x => x.BuildingId)
            .NotEmpty().WithMessage("Building is required")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Buildings.Any(b => b.Uid == id);
                return found;
            }).WithMessage("Building not found");

    }
}