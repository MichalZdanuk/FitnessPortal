namespace FitnessPortalAPI.Repositories;
public interface ICalculatorRepository
{
	Task AddBmiAsync(BMI bmi);
	Task<List<BMI>> GetBMIsForUserPaginatedAsync(int userId, int pageNumber, int pageSize);
	Task<int> GetTotalBMIsCountForUserAsync(int userId);
}
