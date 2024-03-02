namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Api.Controllers.Models;
using CampusHub.Services.Assessments;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class AssessmentController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IAssessmentService assessmentService;
    private readonly IMapper mapper;

    public AssessmentController(IAppLogger logger, IAssessmentService assessmentService, IMapper mapper)
    {
        this.logger = logger;
        this.assessmentService = assessmentService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<AssessmentModel>> GetAll()
    {
        var result = await assessmentService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await assessmentService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<AssessmentModel> Create(CreateModel request)
    {
        var res = await assessmentService.Create(request);

        return res;
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateModel request)
    {
        await assessmentService.Update(id, request);
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await assessmentService.Delete(id);
    }

}
