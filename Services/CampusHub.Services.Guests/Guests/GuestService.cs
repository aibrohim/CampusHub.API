using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Guests;

public class GuestService : IGuestService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public GuestService(
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

    public async Task<IEnumerable<GuestModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var guests = await context.Guests.ToListAsync();

        var result = mapper.Map<IEnumerable<GuestModel>>(guests);

        return result;
    }

    public async Task<GuestModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var guest = await context.Guests.FirstOrDefaultAsync(c => c.Uid == id);

        return mapper.Map<GuestModel>(guest);
    }

    public async Task<GuestModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var guest = mapper.Map<Guest>(model);

        await context.Guests.AddAsync(guest);

        await context.SaveChangesAsync();

        return mapper.Map<GuestModel>(guest);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var guest = await context.Guests.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (guest == null)
            throw new ProcessException($"guest (ID = {id}) not found.");

        guest = mapper.Map(model, guest);

        context.Guests.Update(guest);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var guest = await context.Guests.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (guest == null)
            throw new ProcessException($"guest (ID = {id}) not found.");

        context.Guests.Remove(guest);

        await context.SaveChangesAsync();
    }
}

