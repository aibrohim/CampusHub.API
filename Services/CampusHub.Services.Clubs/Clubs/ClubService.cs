using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Clubs;

public class ClubService : IClubService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public ClubService(
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

    public async Task<IEnumerable<ClubModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var clubs = await context.Clubs.ToListAsync();

        var result = mapper.Map<IEnumerable<ClubModel>>(clubs);

        return result;
    }

    public async Task<ClubModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var club = await context.Clubs.FirstOrDefaultAsync(c => c.Uid == id);

        return mapper.Map<ClubModel>(club);
    }

    public async Task<ClubModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var club = mapper.Map<Club>(model);

        await context.Clubs.AddAsync(club);

        await context.SaveChangesAsync();

        return mapper.Map<ClubModel>(club);
    }

    public async Task<ClubModel> Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var club = await context.Clubs.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (club == null)
            throw new ProcessException($"club (ID = {id}) not found.");

        club = mapper.Map(model, club);

        context.Clubs.Update(club);

        await context.SaveChangesAsync();

        return mapper.Map<ClubModel>(club);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var club = await context.Clubs.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (club == null)
            throw new ProcessException($"club (ID = {id}) not found.");

        context.Clubs.Remove(club);

        await context.SaveChangesAsync();
    }
}

