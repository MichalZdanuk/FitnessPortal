using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Repositories
{
    public interface ICalculatorRepository
    {
        Task AddBmiAsync(BMI bmi);
        Task<List<BMI>> GetBMIsForUserPaginated(int userId, int pageNumber, int pageSize);
        Task<int> GetTotalBMIsCountForUser(int userId);
    }
}
