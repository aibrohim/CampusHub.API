using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.ClubMeetings;

public class ClubMeetingService : IClubMeetingService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public ClubMeetingService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateModel> createModelValidator,
        IModelValidator<UpdateModel> updateModelValidator
        )
	{
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
    }

    public async Task<IEnumerable<ClubMeetingModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var clubMeetings = await context.ClubMeetings
            .Include(cm => cm.Club)
            .Include(cm => cm.Room)
            .Include(cm => cm.Participants)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<ClubMeetingModel>>(clubMeetings);

        return result;
    }

    public async Task<ClubMeetingModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var clubMeeting = await context.ClubMeetings
            .Include(cm => cm.Club)
            .Include(cm => cm.Room)
            .Include(cm => cm.Participants)
            .FirstOrDefaultAsync(c => c.Uid == id);

        return mapper.Map<ClubMeetingModel>(clubMeeting);
    }

    public async Task<ClubMeetingModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var clubMeeting = mapper.Map<ClubMeeting>(model);

        await context.ClubMeetings.AddAsync(clubMeeting);

        await context.SaveChangesAsync();

        return mapper.Map<ClubMeetingModel>(clubMeeting);
    }

    public async Task<ClubMeetingModel> Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var clubMeeting = await context.ClubMeetings.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (clubMeeting == null)
            throw new ProcessException($"clubMeeting (ID = {id}) not found.");

        clubMeeting = mapper.Map(model, clubMeeting);

        context.ClubMeetings.Update(clubMeeting);

        await context.SaveChangesAsync();

        return mapper.Map<ClubMeetingModel>(clubMeeting);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var clubMeeting = await context.ClubMeetings.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (clubMeeting == null)
            throw new ProcessException($"clubMeeting (ID = {id}) not found.");

        context.ClubMeetings.Remove(clubMeeting);

        await context.SaveChangesAsync();
    }
}

