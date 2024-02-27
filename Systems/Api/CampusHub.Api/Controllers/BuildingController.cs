namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Services.Buildings;
using CampusHub.Api.Controllers.Models;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class BuildingController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IBuildingService buildingService;
    private readonly IMapper mapper;

    public BuildingController(IAppLogger logger, IBuildingService buildingService, IMapper mapper)
    {
        this.logger = logger;
        this.buildingService = buildingService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<BuildingModelRes>> GetAll()
    {
        var result = await buildingService.GetAll();

        return mapper.Map<IEnumerable<BuildingModelRes>>(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await buildingService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<BuildingModelRes> Create(CreateBuildingReqModel request)
    {
        var res = await buildingService.Create(mapper.Map<CreateModel>(request));

        return mapper.Map<BuildingModelRes>(res);
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateBuildingReqModel request)
    {
        await buildingService.Update(id, mapper.Map<UpdateModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await buildingService.Delete(id);
    }

}
