using FitnessPortalAPI.Models.Friendship;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/friendship")]
    [ApiController]
    [Authorize]
    public class FriendshipController(IFriendshipService friendshipService)
        : ControllerBase
    {
        [HttpPost("request/{userToBeRequestedId}")]
        public async Task<ActionResult> SendFriendshipRequest([FromRoute] int userToBeRequestedId)
        {
            var requestId = await friendshipService.SendFriendshipRequestAsync(userToBeRequestedId);

            return Created($"/api/friendship/request/{requestId}", null);
        }

        [HttpGet("friendship-requests")]
        public async Task<ActionResult<IEnumerable<FriendshipDto>>> GetFriendshipRequests()
        {
            var friendshipRequests = await friendshipService.GetFriendshipRequestsAsync();

            return Ok(friendshipRequests);
        }

        [HttpPost("accept/{requestId}")]
        public async Task<ActionResult> AcceptFriendRequest([FromRoute] int requestId)
        {
            await friendshipService.AcceptFriendshipRequestAsync(requestId);

            return NoContent();
        }

        [HttpDelete("reject/{requestId}")]
        public async Task<ActionResult> RejectFriendRequest([FromRoute] int requestId)
        {
            await friendshipService.RejectFriendshipRequestAsync(requestId);

            return NoContent();
        }

        [HttpGet("friends")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriends()
        {
            var friends = await friendshipService.GetFriendsAsync();

            return Ok(friends);
        }

        [HttpDelete("remove/{userToBeRemovedId}")]
        public async Task<ActionResult> RemoveFriendship([FromRoute] int userToBeRemovedId)
        {
            await friendshipService.RemoveFriendshipAsync(userToBeRemovedId);

            return NoContent();
        }

        [HttpGet("matching-users")]
        public async Task<ActionResult<IEnumerable<MatchingUserDto>>> FindMatchingUsers([FromQuery] string pattern)
        {
            var users = await friendshipService.FindUsersWithPatternAsync(pattern);

            return Ok(users);
        }

        [HttpGet("friend/{friendId}")]
        public async Task<ActionResult<FriendProfileDto>> GetFriendStatistics([FromRoute] int friendId)
        {
            var friendStats = await friendshipService.GetFriendStatisticsAsync(friendId);

            return Ok(friendStats);
        }
    }
}
