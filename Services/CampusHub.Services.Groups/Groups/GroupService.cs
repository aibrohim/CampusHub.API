using System;
using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Groups;

public class GroupService : IGroupService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public GroupService(
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

    public async Task<IEnumerable<GroupModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var groups = await context.Groups.Include(b => b.Course).ToListAsync();

        var result = mapper.Map<IEnumerable<GroupModel>>(groups);

        return result;
    }

    public async Task<IEnumerable<GroupModel>> GetAllByCourse(Guid courseId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var groups = await context.Groups
            .Include(g => g.Course)
            .Where(g => g.Course.Uid == courseId)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<GroupModel>>(groups);

        return result;
    }

    public async Task<GroupModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var group = await context.Groups
            .Include(g => g.Course)
            .Include(g => g.Students)
            .FirstOrDefaultAsync(g => g.Uid == id);

        return mapper.Map<GroupModel>(group);
    }

    public async Task<GroupModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var group = mapper.Map<Group>(model);

        await context.Groups.AddAsync(group);

        await context.SaveChangesAsync();

        return mapper.Map<GroupModel>(group);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var group = await context.Groups.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (group == null)
            throw new ProcessException($"group (ID = {id}) not found.");

        group = mapper.Map(model, group);

        context.Groups.Update(group);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var group = await context.Groups.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (group == null)
            throw new ProcessException($"group (ID = {id}) not found.");

        context.Groups.Remove(group);

        await context.SaveChangesAsync();
    }
}

