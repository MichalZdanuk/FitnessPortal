using FitnessPortalAPI.Models.Friendship;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<ActionResult> SendFriendshipRequest([FromRoute]int userToBeRequestedId)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var requestId = await _friendshipService.SendFriendshipRequest(userId,userToBeRequestedId);

            return Created($"/api/friendship/request/{requestId}",null);
        }

        [HttpGet("friendship-requests")]
        public async Task<ActionResult<IEnumerable<FriendshipDto>>> GetFriendshipRequests() 
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var friendshipRequests = await _friendshipService.GetFriendshipRequests(userId);
            
            return Ok(friendshipRequests);
        }

        [HttpPost("accept/{requestId}")]
        public async Task<ActionResult> AcceptFriendRequest([FromRoute]int requestId)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _friendshipService.AcceptFriendRequest(userId, requestId);

            return NoContent();
        }

        [HttpDelete("reject/{requestId}")]
        public async Task<ActionResult> RejectFriendRequest([FromRoute]int requestId)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _friendshipService.RejectFriendRequest(userId, requestId);

            return NoContent();
        }

        [HttpGet("friends")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriends()
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var friends = await _friendshipService.GetFriends(userId);

            return Ok(friends);
        }

        [HttpGet("friend/{friendId}")]
        public ActionResult GetFriendData(int friendId)
        {
            return Ok();
        }

        [HttpDelete("remove/{userToBeRemovedId}")]
        public async Task<ActionResult> RemoveFriendship([FromRoute]int userToBeRemovedId)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _friendshipService.RemoveFriendship(userId, userToBeRemovedId);

            return NoContent();
        }

        [HttpGet("matching-users")]
        public async Task<ActionResult<IEnumerable<MatchingUserDto>>> FindMatchingUsers([FromQuery] string pattern)
        {
            //var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var users = await _friendshipService.FindUsersWithPattern(pattern);

            return Ok(users);
        }
    }
}
