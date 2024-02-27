using System;
using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Buildings;

public class BuildingService : IBuildingService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public BuildingService(
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

    public async Task<IEnumerable<BuildingModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var buildings = await context.Buildings.Include(b => b.Rooms).ToListAsync();

        var result = mapper.Map<IEnumerable<BuildingModel>>(buildings);

        return result;
    }

    public async Task<BuildingModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var building = await context.Buildings.Include(b => b.Rooms).FirstOrDefaultAsync(b => b.Uid == id);

        return mapper.Map<BuildingModel>(building);
    }

    public async Task<BuildingModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var building = mapper.Map<Building>(model);

        await context.Buildings.AddAsync(building);

        await context.SaveChangesAsync();

        return mapper.Map<BuildingModel>(building);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var building = await context.Buildings.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (building == null)
            throw new ProcessException($"building (ID = {id}) not found.");

        building = mapper.Map(model, building);

        context.Buildings.Update(building);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var building = await context.Buildings.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (building == null)
            throw new ProcessException($"building (ID = {id}) not found.");

        context.Buildings.Remove(building);

        await context.SaveChangesAsync();
    }
}

