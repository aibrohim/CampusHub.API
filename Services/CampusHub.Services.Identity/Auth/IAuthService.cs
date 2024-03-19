using System;
namespace CampusHub.Services.Identity;

public interface IAuthService
{
	public Task<LoginResModel> Login(LoginModel loginModel);
	public Task StudentRegister(StudentRegisterModel studentRegisterModel);
}

