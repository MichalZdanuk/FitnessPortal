using FitnessPortalAPI.Models;
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
        public ActionResult SendFriendshipRequest([FromRoute]int userToBeRequestedId)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var requestId = _friendshipService.SendFriendshipRequest(userId,userToBeRequestedId);

            return Created($"/api/friendship/request/{requestId}",null);
        }

        [HttpGet("friendship-requests")]
        public ActionResult<IEnumerable<FriendshipDto>> GetFriendshipRequests() 
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var friendshipRequests = _friendshipService.GetFriendshipRequests(userId);
            
            return Ok(friendshipRequests);
        }

        [HttpPost("accept/{requestId}")]
        public ActionResult AcceptFriendRequest([FromRoute]int requestId)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            _friendshipService.AcceptFriendRequest(userId, requestId);

            return NoContent();
        }

        [HttpDelete("reject/{requestId}")]
        public ActionResult RejectFriendRequest([FromRoute]int requestId)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            _friendshipService.RejectFriendRequest(userId, requestId);

            return NoContent();
        }

        [HttpGet("friends")]
        public ActionResult<IEnumerable<FriendDto>> GetFriends()
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var friends = _friendshipService.GetFriends(userId);

            return Ok(friends);
        }

        [HttpGet("friend/{friendId}")]
        public ActionResult GetFriendData(int friendId)
        {
            return Ok();
        }

        [HttpDelete("remove/{userToBeRemovedId}")]
        public ActionResult RemoveFriendship([FromRoute]int userToBeRemovedId)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            _friendshipService.RemoveFriendship(userId, userToBeRemovedId);

            return NoContent();
        }
    }
}
