using FitnessPortalAPI.Exceptions;
using System.Security.Claims;

namespace FitnessPortalAPI.Utilities
{
    public static class HttpContextExtensions
    {
        public static int EnsureUserId(HttpContext httpContext)
        {
            var userIdClaim = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            throw new ForbiddenException("User ID is missing or invalid.");
        }
    }
}
