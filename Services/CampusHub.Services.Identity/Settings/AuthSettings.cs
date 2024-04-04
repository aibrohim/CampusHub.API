using System;
namespace CampusHub.Services.Identity;

public class AuthSettings
{
	public string Key { get; private set; }
	public string Issuer { get; private set; } 
	public string Audience { get; private set; } 
	public int AccessTokenExpirationInDays { get; private set; }
}

