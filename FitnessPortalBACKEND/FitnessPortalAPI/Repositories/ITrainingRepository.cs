using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Repositories
{
	public interface ITrainingRepository
    {
        Task<int> CreateTrainingAsync(Training training, List<Exercise> exercises);
        Task DeleteTraining(int trainingId);
        Task<Training?> GetTrainingByIdAsync(int trainingId);
        Task<IEnumerable<Training>> GetPaginatedTrainingsForUserAsync(int userId, TrainingQuery query);
        Task<IEnumerable<Training>> GetChartDataAsync(int userId, DateTime startDate, DateTime endDate);
        Task<int> GetTotalTrainingsCountForUserAsync(int userId);
        Task<Training?> GetBestTrainingAsync(int userId);
        Task<Training?> GetMostRecentTrainingAsync(int userId);
        Task<User?> GetUserWithTrainings(int userId);
        Task<IEnumerable<Training>> GetRecentTrainingsForUserAsync(int userId, int count);
    }
}
