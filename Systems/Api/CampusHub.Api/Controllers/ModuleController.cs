namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Api.Controllers.Models;
using CampusHub.Services.Modules;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class ModuleController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IModuleService moduleService;
    private readonly IMapper mapper;

    public ModuleController(IAppLogger logger, IModuleService moduleService, IMapper mapper)
    {
        this.logger = logger;
        this.moduleService = moduleService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<ModuleResModel>> GetCourseModules([FromQuery] Guid courseId)
    {
        var result = await moduleService.GetCourseModules(courseId);

        return mapper.Map<IEnumerable<ModuleResModel>>(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await moduleService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<ModuleResModel> Create(CreateModuleReqModel request)
    {
        var res = await moduleService.Create(mapper.Map<CreateModel>(request));

        return mapper.Map<ModuleResModel>(res);
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateModuleReqModel request)
    {
        await moduleService.Update(id, mapper.Map<UpdateModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await moduleService.Delete(id);
    }

}
