namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Services.Identity;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
	private readonly IAppLogger logger;
	private readonly IAuthService authService;
	private readonly IMapper mapper;

	public AuthController(IAppLogger logger, IAuthService authService, IMapper mapper)
	{
		this.logger = logger;
		this.authService = authService;
		this.mapper = mapper;
	}

	[HttpPost("login")]
	public async Task<LoginResModel> Create(LoginModel request)
	{
		return await authService.Login(request);
	}
}
