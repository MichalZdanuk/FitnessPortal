namespace FitnessPortalAPI.Repositories
{
	public interface IAccountRepository
    {
        Task CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(User user);
    }
}
