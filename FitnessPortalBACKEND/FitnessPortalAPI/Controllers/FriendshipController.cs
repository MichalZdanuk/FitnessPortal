using FitnessPortalAPI.Models.Friendship;
using FitnessPortalAPI.Utilities;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/friendship")]
    [ApiController]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IHttpContextAccessor _contextAccessor;

        public FriendshipController(IFriendshipService friendshipService, IHttpContextAccessor contextAccessor)
        {
            _friendshipService = friendshipService;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("request/{userToBeRequestedId}")]
        public async Task<ActionResult> SendFriendshipRequest([FromRoute] int userToBeRequestedId)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var requestId = await _friendshipService.SendFriendshipRequestAsync(userId, userToBeRequestedId);

            return Created($"/api/friendship/request/{requestId}", null);
        }

        [HttpGet("friendship-requests")]
        public async Task<ActionResult<IEnumerable<FriendshipDto>>> GetFriendshipRequests()
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var friendshipRequests = await _friendshipService.GetFriendshipRequestsAsync(userId);

            return Ok(friendshipRequests);
        }

        [HttpPost("accept/{requestId}")]
        public async Task<ActionResult> AcceptFriendRequest([FromRoute] int requestId)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            await _friendshipService.AcceptFriendshipRequestAsync(userId, requestId);

            return NoContent();
        }

        [HttpDelete("reject/{requestId}")]
        public async Task<ActionResult> RejectFriendRequest([FromRoute] int requestId)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            await _friendshipService.RejectFriendshipRequestAsync(userId, requestId);

            return NoContent();
        }

        [HttpGet("friends")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriends()
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var friends = await _friendshipService.GetFriendsForUserAsync(userId);

            return Ok(friends);
        }

        [HttpDelete("remove/{userToBeRemovedId}")]
        public async Task<ActionResult> RemoveFriendship([FromRoute] int userToBeRemovedId)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            await _friendshipService.RemoveFriendshipAsync(userId, userToBeRemovedId);

            return NoContent();
        }

        [HttpGet("matching-users")]
        public async Task<ActionResult<IEnumerable<MatchingUserDto>>> FindMatchingUsers([FromQuery] string pattern)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var users = await _friendshipService.FindUsersWithPatternAsync(userId, pattern);

            return Ok(users);
        }

        [HttpGet("friend/{friendId}")]
        public async Task<ActionResult<FriendProfileDto>> GetFriendStatistics([FromRoute] int friendId)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var friendStats = await _friendshipService.GetFriendStatisticsAsync(userId, friendId);

            return Ok(friendStats);
        }
    }
}
