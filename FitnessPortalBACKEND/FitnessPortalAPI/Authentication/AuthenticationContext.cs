using System.Security.Claims;

namespace FitnessPortalAPI.Authentication;

public class AuthenticationContext(IHttpContextAccessor httpContextAccessor)
	: IAuthenticationContext
{
	public int UserId
	{
		get
		{
			var userIdentifierClaim = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

			if (userIdentifierClaim is not null && int.TryParse(userIdentifierClaim.Value, out int userId))
			{
				return userId;
			}

			throw new Exception("Could not find nameIdentifier claim");
		}
	}

	public string UserRole
	{
		get
		{
			var userRoleClaim = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role);

			if (userRoleClaim is not null)
			{
				return userRoleClaim.Value;
			}

			throw new Exception("Could not find role claim");
		}
	}
	
}
