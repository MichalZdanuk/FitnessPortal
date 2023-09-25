using FitnessPortalAPI.DAL;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.Friendship;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly FitnessPortalDbContext _context;

        public FriendshipService(FitnessPortalDbContext context)
        {
            _context = context;
        }
        public async Task<int> SendFriendshipRequest(int userId, int userToBeRequestedId)
        {
            //checking if receiver is in db
            var receiverUser = await _context.Users.FindAsync(userToBeRequestedId);
            if (receiverUser == null)
                throw new BadRequestException("Receiver user does not exist.");

            //checking if such friendship request has already been sent
            var existingRequest = await _context.FriendshipRequests.FirstOrDefaultAsync(fr => fr.SenderId == userId && fr.ReceiverId == userToBeRequestedId);
            if (existingRequest != null)
                throw new BadRequestException("A friend request has already been sent between these users.");

            //checking if users are already friends
            var areFriends = await _context.Users
                .AnyAsync(u => u.Id == userId && u.Friends.Any(f => f.Id == userToBeRequestedId));
            if (areFriends)
                throw new BadRequestException("These users are already friends.");

            var friendRequest = new FriendshipRequest
            {
                SenderId = userId,
                ReceiverId = userToBeRequestedId,
                SendDate = DateTime.Now,
            };

            _context.FriendshipRequests.Add(friendRequest);
            await _context.SaveChangesAsync();

            return friendRequest.Id;
        }

        public async Task<List<FriendshipDto>> GetFriendshipRequests(int userId)
        {
            var friendshipRequests = await _context.FriendshipRequests
                .Include(fr => fr.Sender)
                .Include(fr => fr.Receiver)
                .Where(fr => fr.ReceiverId == userId)
                .ToListAsync();

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

        public async Task RejectFriendRequest(int userId, int requestId)
        {
            var friendRequest = await _context.FriendshipRequests.FindAsync(requestId);
            if (friendRequest == null)
                throw new BadRequestException("Friend request not found");

            if (friendRequest.ReceiverId != userId)
                throw new ForbiddenException("You are not allowed to remove someone else request!!!");

            _context.FriendshipRequests.Remove(friendRequest);
            await _context.SaveChangesAsync();
        }

        public async Task AcceptFriendRequest(int userId, int requestId)
        {
            var friendRequest = await _context.FriendshipRequests
                .Include(fr => fr.Sender)
                    .ThenInclude(u => u.Friends)
                .Include(fr => fr.Receiver)
                    .ThenInclude(u => u.Friends)
                .FirstOrDefaultAsync(fr => fr.Id == requestId);

            if (friendRequest == null)
                throw new BadRequestException("Friend request not found");

            var sender = friendRequest.Sender;
            var receiver = friendRequest.Receiver;

            if (sender == null || receiver == null)
                throw new BadRequestException("Invalid sender or receiver");

            if (friendRequest.ReceiverId != userId)
                throw new ForbiddenException("You are not allowed to accept someone else request!!!");

            sender.Friends.Add(receiver);
            receiver.Friends.Add(sender);

            _context.FriendshipRequests.Remove(friendRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FriendDto>> GetFriends(int userId)
        {
            Thread.Sleep(1000);//added to present loading spinner in client app
            var user = await _context.Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException("User not found");

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
            var user = await _context.Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new BadRequestException("User not found");

            var friendToBeRemoved = _context.Users
                .Include(f => f.Friends)
                .FirstOrDefault(f => f.Id == userToBeRemovedId);


            if (friendToBeRemoved == null)
                throw new BadRequestException("Friend not found");

            user.Friends.Remove(friendToBeRemoved);
            friendToBeRemoved.Friends.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<List<MatchingUserDto>> FindUsersWithPattern(string pattern)
        {
            var users = await _context.Users
                .Where(user => user.Email.Contains(pattern))
                .Select(user => new MatchingUserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                })
                .ToListAsync();

            return users;
        }
    }
}
