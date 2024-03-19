using System;
namespace CampusHub.Services.Identity;

public class TokenModel
{
	public string AccessToken { get; set; }

	public DateTime? ExpireDate { get; set; }
}

