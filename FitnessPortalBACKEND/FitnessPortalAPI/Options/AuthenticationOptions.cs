namespace FitnessPortalAPI.Options;
public class AuthenticationOptions
{
	public string JwtKey { get; set; }
	public int JwtExpireDays { get; set; }
	public string JwtIssuer { get; set; }
}
