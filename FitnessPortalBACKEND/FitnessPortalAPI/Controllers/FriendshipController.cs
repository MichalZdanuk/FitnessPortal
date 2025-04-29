using FitnessPortalAPI.Models.Friendship;
using FitnessPortalAPI.Utilities;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/friendship")]
    [ApiController]
    [Authorize]
    public class FriendshipController(IFriendshipService friendshipService, IHttpContextAccessor contextAccessor)
        : ControllerBase
    {
        [HttpPost("request/{userToBeRequestedId}")]
        public async Task<ActionResult> SendFriendshipRequest([FromRoute] int userToBeRequestedId)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var requestId = await friendshipService.SendFriendshipRequest(userId, userToBeRequestedId);

            return Created($"/api/friendship/request/{requestId}", null);
        }

        [HttpGet("friendship-requests")]
        public async Task<ActionResult<IEnumerable<FriendshipDto>>> GetFriendshipRequests()
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var friendshipRequests = await friendshipService.GetFriendshipRequests(userId);

            return Ok(friendshipRequests);
        }

        [HttpPost("accept/{requestId}")]
        public async Task<ActionResult> AcceptFriendRequest([FromRoute] int requestId)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            await friendshipService.AcceptFriendshipRequest(userId, requestId);

            return NoContent();
        }

        [HttpDelete("reject/{requestId}")]
        public async Task<ActionResult> RejectFriendRequest([FromRoute] int requestId)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            await friendshipService.RejectFriendshipRequest(userId, requestId);

            return NoContent();
        }

        [HttpGet("friends")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriends()
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var friends = await friendshipService.GetFriendsForUser(userId);

            return Ok(friends);
        }

        [HttpDelete("remove/{userToBeRemovedId}")]
        public async Task<ActionResult> RemoveFriendship([FromRoute] int userToBeRemovedId)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            await friendshipService.RemoveFriendship(userId, userToBeRemovedId);

            return NoContent();
        }

        [HttpGet("matching-users")]
        public async Task<ActionResult<IEnumerable<MatchingUserDto>>> FindMatchingUsers([FromQuery] string pattern)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var users = await friendshipService.FindUsersWithPattern(userId, pattern);

            return Ok(users);
        }

        [HttpGet("friend/{friendId}")]
        public async Task<ActionResult<FriendProfileDto>> GetFriendStatistics([FromRoute] int friendId)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var friendStats = await friendshipService.GetFriendStatistics(userId, friendId);

            return Ok(friendStats);
        }
    }
}
