namespace FitnessPortalAPI.DAL.Repositories;
public class AccountRepository(FitnessPortalDbContext dbContext)
		: IAccountRepository
{
	public async Task CreateUserAsync(User user)
	{
		await dbContext.Users.AddAsync(user);
		await dbContext.SaveChangesAsync();
	}

	public async Task<User?> GetUserByEmailAsync(string email)
	{
		return await dbContext.Users
							.Include(u => u.Role)
							.FirstOrDefaultAsync(u => u.Email == email);
	}

	public async Task<User?> GetUserByIdAsync(int userId)
	{
		return await dbContext.Users
							.Include(u => u.Role)
							.Include(u => u.Friends)
							.FirstOrDefaultAsync(u => u.Id == userId);
	}

	public async Task UpdateUserAsync(User user)
	{
		dbContext.Entry(user).State = EntityState.Modified;
		await dbContext.SaveChangesAsync();
	}
}
