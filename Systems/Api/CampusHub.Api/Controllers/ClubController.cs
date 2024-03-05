namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Services.Clubs;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class ClubController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IClubService clubService;
    private readonly IMapper mapper;

    public ClubController(IAppLogger logger, IClubService clubService, IMapper mapper)
    {
        this.logger = logger;
        this.clubService = clubService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<ClubModel>> GetAll()
    {
        return await clubService.GetAll();
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await clubService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<ClubModel> Create(CreateModel request)
    {
        return await clubService.Create(request);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ClubModel> Update([FromRoute] Guid id, UpdateModel request)
    {
        return await clubService.Update(id, mapper.Map<UpdateModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await clubService.Delete(id);
    }

}
