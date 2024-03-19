namespace CampusHub.Services.Identity;

using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using CampusHub.Settings;
using CampusHub.Context;
using CampusHub.Context.Entities;

public static class IS4Config
{
	public static IServiceCollection AddIS4(this IServiceCollection services, IConfiguration configuration = null)
	{
		var settings = Settings.Load<AuthSettings>("Auth");
		services.AddSingleton(settings);

		services
				.AddIdentity<User, IdentityRole<Guid>>(options =>
				{
					options.Password.RequiredLength = 4;
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireUppercase = false;
					options.Password.RequireNonAlphanumeric = false;
				})
				.AddEntityFrameworkStores<MainDbContext>()
				.AddDefaultTokenProviders();

		services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidAudience = settings.Audience,
						ValidIssuer = settings.Issuer,
						RequireExpirationTime = true,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(settings.Key!)
						),
						ValidateIssuerSigningKey = true
					};
				});

		return services;
	}
}

