using CampusHub.Services.Email;

namespace CampusHub.Services.Identity;

using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

using Common.Validator;
using Context;
using Context.Entities;
using Common.Exceptions;

public class AuthService: IAuthService
{
	private readonly IModelValidator<LoginModel> _loginModelValidator;
	private readonly IModelValidator<ForgotPasswordModel> _forgotPasswordModelValidator;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IEmailService _emailService;
	private readonly IMapper _mapper;
	private readonly AuthSettings _authSettings;

	public AuthService(
		IModelValidator<LoginModel> loginModelValidator,
		IModelValidator<ForgotPasswordModel> forgotPasswordModelValidator,
		UserManager<User> userManager,
		SignInManager<User> signInManager,
		IEmailService emailService,
		IMapper mapper,
		AuthSettings authSettings
	)
	{
		_loginModelValidator = loginModelValidator;
		_forgotPasswordModelValidator = forgotPasswordModelValidator;
		_userManager = userManager;
		_signInManager = signInManager;
		_emailService = emailService;
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

	public async Task<string> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
	{
		await _forgotPasswordModelValidator.CheckAsync(forgotPasswordModel);
		
		var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordModel.Email);

		if (user == null)
			throw new ProcessException($"{forgotPasswordModel.Email} not found!");

		string recoverPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
		
		_emailService.SendEmail(
			forgotPasswordModel.Email, 
			"Recovery Password", 
			$"Your token is: {recoverPasswordToken}"
			);

		return recoverPasswordToken;
	}

	public async Task RecoverPassword(RecoverPasswordModel recoverPasswordModel)
	{
		var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == recoverPasswordModel.Email);
		
		if (user == null)
			throw new ProcessException($"{recoverPasswordModel.Email} not found!");

		await _userManager.ResetPasswordAsync(
			user, 
			recoverPasswordModel.RecoveryToken, 
			recoverPasswordModel.NewPassword);
	}

	public Task StudentRegister(StudentRegisterModel studentRegisterModel)
	{
		throw new NotImplementedException();
	}
}

