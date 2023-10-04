using FitnessPortalAPI.DAL;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.Friendship;
using FitnessPortalAPI.Repositories;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;

        public FriendshipService(IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
        }
        public async Task<int> SendFriendshipRequest(int userId, int userToBeRequestedId)
        {
            var receiverUser = await _friendshipRepository.GetUserByIdAsync(userToBeRequestedId);
            if (receiverUser == null)
                throw new BadRequestException("Receiver user does not exist.");

            var existingRequest = await _friendshipRepository.FriendshipRequestExistsAsync(userId, userToBeRequestedId);
            if (!existingRequest)
                throw new BadRequestException("A friend request has already been sent between these users.");

            var usersAreFriends = await _friendshipRepository.AreUsersFriendsAsync(userId, userToBeRequestedId);
            if (usersAreFriends)
                throw new BadRequestException("These users are already friends.");

            var friendshipRequest = new FriendshipRequest
            {
                SenderId = userId,
                ReceiverId = userToBeRequestedId,
                SendDate = DateTime.Now,
            };

            await _friendshipRepository.CreateFriendshipRequestAsync(friendshipRequest);

            return friendshipRequest.Id;
        }

        public async Task<IEnumerable<FriendshipDto>> GetFriendshipRequests(int userId)
        {
            var friendshipRequests = await _friendshipRepository.GetFriendshipRequestsForUserAsync(userId);

            var friendshipDtos = friendshipRequests.Select(fr => new FriendshipDto
            {
                Id = fr.Id,
                SenderId = fr.SenderId,
                ReceiverId = fr.ReceiverId,
                SenderName = fr.Sender.Username,
                SendDate = fr.SendDate
            }).ToList();

            return friendshipDtos;
        }

        public async Task RejectFriendshipRequest(int userId, int requestId)
        {
            var friendRequest = await _friendshipRepository.GetFriendshipRequestAsync(requestId);
            if (friendRequest == null)
                throw new BadRequestException("Friend request not found.");

            if (friendRequest.ReceiverId != userId)
                throw new ForbiddenException("You are not allowed to remove someone else request!!!");

            await _friendshipRepository.RemoveFriendshipRequest(friendRequest);
        }

        public async Task AcceptFriendshipRequest(int userId, int requestId)
        {
            var friendshipRequest = await _friendshipRepository.GetFriendshipRequestAsync(requestId);

            if (friendshipRequest == null)
                throw new BadRequestException("Friendship request not found.");

            var sender = friendshipRequest.Sender;
            var receiver = friendshipRequest.Receiver;

            if (sender == null || receiver == null)
                throw new BadRequestException("Invalid sender or receiver.");

            if (friendshipRequest.ReceiverId != userId)
                throw new ForbiddenException("You are not allowed to accept someone else request!!!");

            await _friendshipRepository.AddFriendAsync(sender, receiver);
            await _friendshipRepository.RemoveFriendshipRequest(friendshipRequest);
        }

        public async Task<IEnumerable<FriendDto>> GetFriendsForUser(int userId)
        {
            var user = await _friendshipRepository.GetUserByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found.");

            var friends = user.Friends;

            var friendDtos = friends.Select(friend => new FriendDto
            {
                Id = friend.Id,
                Username = friend.Username,
                Email = friend.Email,
            }).ToList();

            return friendDtos;
        }

        public async Task RemoveFriendship(int userId, int userToBeRemovedId)
        {
            var user = await _friendshipRepository.GetUserByIdAsync(userId);

            if (user == null)
                throw new BadRequestException("User not found.");

            var friendToBeRemoved = await _friendshipRepository.GetUserByIdAsync(userToBeRemovedId);

            if (friendToBeRemoved == null)
                throw new BadRequestException("Friend not found.");

            await _friendshipRepository.RemoveFriendAsync(user, friendToBeRemoved);
        }

        public async Task<IEnumerable<MatchingUserDto>> FindUsersWithPattern(string pattern)
        {
            var users = await _friendshipRepository.FindUsersWithPattern(pattern);

            var matchingUserDtos = users.Select(user => new MatchingUserDto()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
            }).ToList();

            return matchingUserDtos;
        }
    }
}
