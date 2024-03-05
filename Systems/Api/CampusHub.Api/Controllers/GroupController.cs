namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Services.Groups;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IGroupService groupService;
    private readonly IMapper mapper;

    public GroupController(IAppLogger logger, IGroupService groupService, IMapper mapper)
    {
        this.logger = logger;
        this.groupService = groupService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<GroupModel>> GetAll()
    {
        var result = await groupService.GetAll();

        return mapper.Map<IEnumerable<GroupModel>>(result);
    }

    [HttpGet("Course/{courseId:Guid}")]
    public async Task<IEnumerable<GroupModel>> GetAllByCourse([FromRoute] Guid courseId)
    {
        var result = await groupService.GetAllByCourse(courseId);

        return mapper.Map<IEnumerable<GroupModel>>(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await groupService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<GroupModel> Create(CreateModel request)
    {
        var res = await groupService.Create(mapper.Map<CreateModel>(request));

        return mapper.Map<GroupModel>(res);
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateModel request)
    {
        await groupService.Update(id, mapper.Map<UpdateModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await groupService.Delete(id);
    }

}
