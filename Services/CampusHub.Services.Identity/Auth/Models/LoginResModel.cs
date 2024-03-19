using AutoMapper;
using CampusHub.Context.Entities;

namespace CampusHub.Services.Identity;

public class LoginResModel
{
	public string accessToken { get; set; }

	public UserLoginResModel data { get; set; }
}

public class UserLoginResModel
{
	public string Email { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Role { get; set; }
}

public class UserLoginResModelProfile : Profile
{
	public UserLoginResModelProfile()
	{
		CreateMap<User, UserLoginResModel>();
	}
}