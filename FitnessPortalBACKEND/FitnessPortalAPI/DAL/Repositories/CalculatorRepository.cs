namespace FitnessPortalAPI.DAL.Repositories
{
	public class CalculatorRepository : ICalculatorRepository
    {
        private readonly FitnessPortalDbContext _dbContext;

        public CalculatorRepository(FitnessPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBmiAsync(BMI bmi)
        {
            await _dbContext.BMIs.AddAsync(bmi);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BMI>> GetBMIsForUserPaginated(int userId, int pageNumber, int pageSize)
        {
            return await _dbContext.BMIs
                            .Where(b => b.UserId == userId)
                            .OrderByDescending(b => b.Date)
                            .Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .ToListAsync();
        }

        public async Task<int> GetTotalBMIsCountForUser(int userId)
        {
            return await _dbContext.BMIs
                            .Where(b => b.UserId == userId)
                            .CountAsync();
        }
    }
}
