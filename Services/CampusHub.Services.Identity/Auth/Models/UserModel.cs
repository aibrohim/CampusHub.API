using System;
using AutoMapper;
using CampusHub.Context.Entities;

namespace CampusHub.Services.Identity;

public class UserModel
{
	public string Email { get; set; }
	public UserRole Role { get; set; }
}

public class UserModelProfile : Profile
{
	public UserModelProfile()
	{
		CreateMap<User, UserModel>();
	}
}