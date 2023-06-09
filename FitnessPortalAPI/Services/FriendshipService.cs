using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.Services
{
    public interface IFriendshipService
    {
        int SendFriendshipRequest(int userId, int userToBeRequestedId);
        List<FriendshipDto> GetFriendshipRequests(int userId);
        void RejectFriendRequest(int userId, int requestId);
        void AcceptFriendRequest(int userId, int requestId);
        List<FriendDto> GetFriends(int userId);
        void RemoveFriendship(int userId, int userToBeRemovedId);
    }
    public class FriendshipService : IFriendshipService
    {
        private readonly FitnessPortalDbContext _context;

        public FriendshipService(FitnessPortalDbContext context)
        {
            _context = context;
        }
        public int SendFriendshipRequest(int userId, int userToBeRequestedId)
        {
            //checking if receiver is in db
            var receiverUser = _context.Users.Find(userToBeRequestedId);
            if (receiverUser == null)
                throw new BadRequestException("Receiver user does not exist.");

            //checking if such friendship request has already been sent
            var existingRequest = _context.FriendshipRequests.FirstOrDefault(fr => fr.SenderId == userId && fr.ReceiverId == userToBeRequestedId);
            if(existingRequest != null)
                throw new BadRequestException("A friend request has already been sent between these users.");

            //checking if users are already friends
            var areFriends = _context.Users
                .Any(u => u.Id == userId && u.Friends.Any(f => f.Id == userToBeRequestedId));
            if (areFriends)
                throw new BadRequestException("These users are already friends.");

            var friendRequest = new FriendshipRequest
            {
                SenderId = userId,
                ReceiverId = userToBeRequestedId,
                SendDate = DateTime.Now,
            };

            _context.FriendshipRequests.Add(friendRequest);
            _context.SaveChanges();

            return friendRequest.Id;
        }

        public List<FriendshipDto> GetFriendshipRequests(int userId)
        {
            var friendshipRequests = _context.FriendshipRequests
                .Include(fr => fr.Sender)
                .Include(fr => fr.Receiver)
                .Where(fr => fr.ReceiverId == userId)
                .ToList();

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

        public void RejectFriendRequest(int userId, int requestId)
        {
            var friendRequest = _context.FriendshipRequests.Find(requestId);
            if (friendRequest == null)
                throw new BadRequestException("Friend request not found");

            if (friendRequest.ReceiverId != userId)
                throw new ForbiddenException("You are not allowed to remove someone else request!!!");

            _context.FriendshipRequests.Remove(friendRequest);
            _context.SaveChanges();
        }

        public void AcceptFriendRequest(int userId, int requestId)
        {
            var friendRequest = _context.FriendshipRequests
                .Include(fr => fr.Sender)
                    .ThenInclude(u => u.Friends)
                .Include(fr => fr.Receiver)
                    .ThenInclude(u => u.Friends)
                .FirstOrDefault(fr => fr.Id == requestId);

            if(friendRequest == null)
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
            _context.SaveChanges();
        }

        public List<FriendDto> GetFriends(int userId)
        {
            var user = _context.Users
                .Include(u => u.Friends)
                .FirstOrDefault(u => u.Id == userId);

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

        public void RemoveFriendship(int userId, int userToBeRemovedId)
        {
            var user = _context.Users
                .Include(u => u.Friends)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
                throw new BadRequestException("User not found");

            var friendToBeRemoved = _context.Users
                .Include(f => f.Friends)
                .FirstOrDefault(f => f.Id == userToBeRemovedId);


            if (friendToBeRemoved == null)
                throw new BadRequestException("Friend not found");

            user.Friends.Remove(friendToBeRemoved);
            friendToBeRemoved.Friends.Remove(user);

            _context.SaveChanges();
        }

    }
}
