using FitnessPortalAPI.Models.Friendship;
using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Services
{
	public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IMapper _mapper;

        public FriendshipService(IFriendshipRepository friendshipRepository, IMapper mapper)
        {
            _friendshipRepository = friendshipRepository;
            _mapper = mapper;
        }

        public async Task<int> SendFriendshipRequest(int userId, int userToBeRequestedId)
        {
            var receiverUser = await _friendshipRepository.GetUserByIdAsync(userToBeRequestedId);
            if (receiverUser == null)
                throw new BadRequestException("Receiver user does not exist.");

            var existingRequest = await _friendshipRepository.FriendshipRequestExistsAsync(userId, userToBeRequestedId);
            if (existingRequest)
                throw new BadRequestException("Frienship request has already been sent between these users.");

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

            var friendshipDtos = _mapper.Map<List<FriendshipDto>>(friendshipRequests);

            return friendshipDtos;
        }

        public async Task RejectFriendshipRequest(int userId, int requestId)
        {
            var friendRequest = await _friendshipRepository.GetFriendshipRequestAsync(requestId);
            if (friendRequest == null)
                throw new BadRequestException("Friendship request not found.");

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

            var friendsDtos = _mapper.Map<IEnumerable<FriendDto>>(friends);

            return friendsDtos;
        }

        public async Task RemoveFriendship(int userId, int userToBeRemovedId)
        {
            var user = await _friendshipRepository.GetUserByIdAsync(userId);

            if (user == null)
                throw new BadRequestException("User not found.");

            var friendToBeRemoved = await _friendshipRepository.GetUserByIdAsync(userToBeRemovedId);

            if (friendToBeRemoved == null)
                throw new BadRequestException("Friend not found.");

            var usersAreFriends = await _friendshipRepository.AreUsersFriendsAsync(userId, userToBeRemovedId);

            if (!usersAreFriends)
            {
                throw new BadRequestException("You are not friend with this user.");
            }

            await _friendshipRepository.RemoveFriendAsync(user, friendToBeRemoved);
        }

        public async Task<IEnumerable<MatchingUserDto>> FindUsersWithPattern(int userId, string pattern)
        {
            var users = await _friendshipRepository.FindUsersWithPattern(userId, pattern);

            var matchingUsersDtos = _mapper.Map<IEnumerable<MatchingUserDto>>(users);

            return matchingUsersDtos;
        }

        public async Task<FriendProfileDto> GetFriendStatistics(int userId, int friendId)
        {
            var friend = await _friendshipRepository.GetUserByIdAsync(friendId);

            if (friend == null)
                throw new BadRequestException("Friend not found.");

            var usersAreFriends = await _friendshipRepository.AreUsersFriendsAsync(userId, friendId);
            if (!usersAreFriends)
                throw new ForbiddenException("Given user is not your friend.");

            var friendTrainings = await _friendshipRepository.GetFriendTrainingsAsync(friendId);

            var friendProfileDto = _mapper.Map<User, FriendProfileDto>(friend);
            friendProfileDto.NumberOfFriends = friend.Friends.Count();
            friendProfileDto.NumberOfTrainings = friendTrainings.Count();
            friendProfileDto.LastThreeTrainings = _mapper.Map<List<TrainingDto>>(friendTrainings);

            return friendProfileDto;
        }
    }
}
