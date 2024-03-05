namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Services.ClubMeetings;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class ClubMeetingController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IClubMeetingService clubMeetingService;
    private readonly IMapper mapper;

    public ClubMeetingController(IAppLogger logger, IClubMeetingService clubMeetingService, IMapper mapper)
    {
        this.logger = logger;
        this.clubMeetingService = clubMeetingService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<ClubMeetingModel>> GetAll()
    {
        return await clubMeetingService.GetAll();
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await clubMeetingService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<ClubMeetingModel> Create(CreateModel request)
    {
        return await clubMeetingService.Create(request);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ClubMeetingModel> Update([FromRoute] Guid id, UpdateModel request)
    {
        return await clubMeetingService.Update(id, request);
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await clubMeetingService.Delete(id);
    }

}
