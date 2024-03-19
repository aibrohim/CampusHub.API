using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CampusHub.Services.Identity;

public class JWTTokenFactory
{
	public static TokenModel Generate(UserModel user, AuthSettings authSettings)
	{
		var claims = new List<Claim> { new(ClaimTypes.Email, user.Email!) };
		claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()!));

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key));

		var token = new JwtSecurityToken(
			issuer: authSettings.Issuer,
			audience: authSettings.Audience,
			claims: claims,
			expires: DateTime.Now.AddDays(authSettings.AccessTokenExpirationInDays),
			signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
		);

		string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

		return new TokenModel { AccessToken = tokenAsString, ExpireDate = token.ValidTo };
	}
}

