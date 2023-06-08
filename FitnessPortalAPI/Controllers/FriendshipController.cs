using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/friendship")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        public FriendshipController(IFriendshipService friendshipService) 
        {
            _friendshipService = friendshipService;
        }

        [HttpPost("request")]
        public ActionResult SendFriendshipRequest([FromBody]CreateFriendshipRequestDto dto)
        {
            var requestId = _friendshipService.SendFriendshipRequest(dto);

            return Created($"/api/friendship/request/{requestId}",null);
        }

        [HttpGet("friendship-requests/{userId}")]
        public ActionResult<IEnumerable<FriendshipDto>> GetFriendshipRequests([FromRoute]int userId) 
        {
            var friendshipRequests = _friendshipService.GetFriendshipRequests(userId);
            
            return Ok(friendshipRequests);
        }

        [HttpPost("accept/{requestId}")]
        public ActionResult AcceptFriendRequest([FromRoute]int requestId)
        {
            _friendshipService.AcceptFriendRequest(requestId);

            return NoContent();
        }

        [HttpDelete("reject/{requestId}")]
        public ActionResult RejectFriendRequest([FromRoute]int requestId)
        {
            _friendshipService.RejectFriendRequest(requestId);

            return NoContent();
        }

        [HttpGet("friends/{userId}")]
        public ActionResult<IEnumerable<FriendDto>> GetFriends([FromRoute]int userId)
        {
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
            _friendshipService.RemoveFriendship(userToBeRemovedId);

            return NoContent();
        }
    }
}
