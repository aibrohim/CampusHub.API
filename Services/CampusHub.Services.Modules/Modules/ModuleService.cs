using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Modules;

public class ModuleService : IModuleService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public ModuleService(
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

    public async Task<IEnumerable<ModuleModel>> GetCourseModules(Guid courseId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var modules = await context.Modules.Where(r => r.Course.Uid == courseId).ToListAsync();

        var result = mapper.Map<IEnumerable<ModuleModel>>(modules);

        return result;
    }

    public async Task<ModuleModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var module = await context.Modules.FirstOrDefaultAsync(r => r.Uid == id);

        return mapper.Map<ModuleModel>(module);
    }

    public async Task<ModuleModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var module = mapper.Map<Module>(model);

        await context.Modules.AddAsync(module);

        await context.SaveChangesAsync();

        return mapper.Map<ModuleModel>(module);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var module = await context.Modules.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (module == null)
            throw new ProcessException($"module (ID = {id}) not found.");

        module = mapper.Map(model, module);

        context.Modules.Update(module);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var module = await context.Modules.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (module == null)
            throw new ProcessException($"module (ID = {id}) not found.");

        context.Modules.Remove(module);

        await context.SaveChangesAsync();
    }
}

