﻿namespace FitnessPortalAPI.DAL.Repositories;
public class FriendshipRepository(FitnessPortalDbContext dbContext)
		: IFriendshipRepository
{
	public async Task AddFriendAsync(User firstUser, User secondUser)
	{
		firstUser.Friends.Add(secondUser);
		secondUser.Friends.Add(firstUser);
		await dbContext.SaveChangesAsync();
	}

	public async Task<bool> AreUsersFriendsAsync(int firstUserId, int secondUserId)
	{
		return await dbContext.Users
						.AnyAsync(u => u.Id == firstUserId && u.Friends.Any(f => f.Id == secondUserId));
	}

	public async Task CreateFriendshipRequestAsync(FriendshipRequest friendshipRequest)
	{
		await dbContext.FriendshipRequests.AddAsync(friendshipRequest);
		await dbContext.SaveChangesAsync();
	}

	public async Task<FriendshipRequest?> GetFriendshipRequestAsync(int requestId)
	{
		return await dbContext.FriendshipRequests
						.Include(fr => fr.Sender)
						.ThenInclude(u => u.Friends)
						.Include(fr => fr.Receiver)
						.ThenInclude(u => u.Friends)
						.FirstOrDefaultAsync(fr => fr.Id == requestId);
	}

	public async Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestsForUserAsync(int userId)
	{
		return await dbContext.FriendshipRequests
						.Include(fr => fr.Sender)
						.Include(fr => fr.Receiver)
						.Where(fr => fr.ReceiverId == userId)
						.ToListAsync();
	}

	public async Task<User?> GetUserByIdAsync(int userId)
	{
		return await dbContext.Users
						.Include(u => u.Friends)
						.FirstOrDefaultAsync(u => u.Id == userId);
	}

	public async Task RemoveFriendAsync(User userToRemove, User friendToRemove)
	{
		userToRemove.Friends.Remove(friendToRemove);
		friendToRemove.Friends.Remove(userToRemove);
		await dbContext.SaveChangesAsync();
	}

	public async Task RemoveFriendshipRequestAsync(FriendshipRequest friendshipRequest)
	{
		dbContext.FriendshipRequests.Remove(friendshipRequest);
		await dbContext.SaveChangesAsync();
	}

	public async Task<IEnumerable<User>> FindUsersWithPatternAsync(int userId, string pattern)
	{
		return await dbContext.Users
			.Where(user =>
				user.Id != userId &&
				user.Email.Contains(pattern) &&
				!user.Friends.Any(friend => friend.Id == userId) &&
				!user.ReceivedFriendRequests.Any(request => request.SenderId == userId))
			.ToListAsync();
	}

	public async Task<bool> FriendshipRequestExistsAsync(int senderId, int receiverId)
	{
		return await dbContext.FriendshipRequests
			.AnyAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId);
	}

	public async Task<IEnumerable<Training>> GetFriendTrainingsAsync(int friendId)
	{
		return await dbContext.Trainings
			.Where(training => training.UserId == friendId)
			.Include(training => training.Exercises)
			.OrderByDescending(training => training.DateOfTraining)
			.Take(3)
			.ToListAsync();
	}
}
