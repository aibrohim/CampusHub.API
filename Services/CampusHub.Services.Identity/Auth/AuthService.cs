namespace CampusHub.Services.Identity;

using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using CampusHub.Common.Exceptions;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

public class AuthService: IAuthService
{
	private readonly IModelValidator<LoginModel> _loginModelValidator;
	private readonly IDbContextFactory<MainDbContext> _dbContextFactory;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IMapper _mapper;
	private readonly AuthSettings _authSettings;

	public AuthService(
		IModelValidator<LoginModel> loginModelValidator,
		IDbContextFactory<MainDbContext> dbContextFactory,
		UserManager<User> userManager,
		SignInManager<User> signInManager,
		IMapper mapper,
		AuthSettings authSettings
	)
	{
		_loginModelValidator = loginModelValidator;
		_dbContextFactory = dbContextFactory;
		_userManager = userManager;
		_signInManager = signInManager;
		_mapper = mapper;
		_authSettings = authSettings;
	}

	public async Task<LoginResModel> Login(LoginModel loginModel)
	{
		await _loginModelValidator.CheckAsync(loginModel);

		var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginModel.Email);

		if (user == null)
			throw new ProcessException($"{loginModel.Email} not found!");

		var passwordCheckRes = await _signInManager.PasswordSignInAsync(
			user,
			loginModel.Password,
			false,
			false
		);

		if (!passwordCheckRes.Succeeded)
			throw new ValidationException(
				"password",
				new List<ValidationFailure>() {
					new ValidationFailure() { PropertyName = "password", ErrorMessage = "Wrong Password" }
				}
			);

		var userModel = _mapper.Map<UserModel>(user);
	
		TokenModel token = JWTTokenFactory.Generate(userModel, _authSettings);

		return new LoginResModel()
		{
			accessToken = token.AccessToken,
			data = _mapper.Map<UserLoginResModel>(user)
		};
	}

	public Task StudentRegister(StudentRegisterModel studentRegisterModel)
	{
		throw new NotImplementedException();
	}
}

