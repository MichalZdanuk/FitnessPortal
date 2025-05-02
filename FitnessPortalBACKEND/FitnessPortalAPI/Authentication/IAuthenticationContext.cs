namespace FitnessPortalAPI.Authentication;

public interface IAuthenticationContext
{
	public int UserId { get; }
	public string UserRole { get; }
}
