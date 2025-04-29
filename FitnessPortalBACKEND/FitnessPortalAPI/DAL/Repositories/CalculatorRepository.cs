namespace FitnessPortalAPI.DAL.Repositories;
public class CalculatorRepository(FitnessPortalDbContext dbContext)
		: ICalculatorRepository
{
	public async Task AddBmiAsync(BMI bmi)
	{
		await dbContext.BMIs.AddAsync(bmi);
		await dbContext.SaveChangesAsync();
	}

	public async Task<List<BMI>> GetBMIsForUserPaginatedAsync(int userId, int pageNumber, int pageSize)
	{
		return await dbContext.BMIs
						.Where(b => b.UserId == userId)
						.OrderByDescending(b => b.Date)
						.Skip(pageSize * (pageNumber - 1))
						.Take(pageSize)
						.ToListAsync();
	}

	public async Task<int> GetTotalBMIsCountForUserAsync(int userId)
	{
		return await dbContext.BMIs
						.Where(b => b.UserId == userId)
						.CountAsync();
	}
}
