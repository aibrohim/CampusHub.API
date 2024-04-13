using CampusHub.Services.Email;
using CampusHub.Services.Settings;
using Microsoft.AspNetCore.Hosting;

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
	private readonly IWebHostEnvironment _env;
	private readonly MainSettings _mainSettings;

	private string PASSWORD_RECOVERY_EMAIL_TEMPLATE_PATH = "PasswordRecoverTemplate.html";

	public AuthService(
		IModelValidator<LoginModel> loginModelValidator,
		IModelValidator<ForgotPasswordModel> forgotPasswordModelValidator,
		UserManager<User> userManager,
		SignInManager<User> signInManager,
		IEmailService emailService,
		IMapper mapper,
		AuthSettings authSettings,
		IWebHostEnvironment env,
		MainSettings mainSettings
	)
	{
		_loginModelValidator = loginModelValidator;
		_forgotPasswordModelValidator = forgotPasswordModelValidator;
		_userManager = userManager;
		_signInManager = signInManager;
		_emailService = emailService;
		_mapper = mapper;
		_authSettings = authSettings;
		_env = env;
		_mainSettings = mainSettings;
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
		
		string template = File.ReadAllText(_env.ContentRootPath + "/" + PASSWORD_RECOVERY_EMAIL_TEMPLATE_PATH);
		string emailContent = template
			.Replace("{RECOVERY_TOKEN}", recoverPasswordToken)
			.Replace("{FRONT_END_URL}", user.Role == UserRole.Admin 
				? _mainSettings.AdminFrontUrl 
				: _mainSettings.StudentFrontUrl);
		
		_emailService.SendEmail(
			forgotPasswordModel.Email, 
			"Password Recovery", 
			emailContent
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

