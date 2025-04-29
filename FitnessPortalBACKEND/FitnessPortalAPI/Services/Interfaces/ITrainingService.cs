using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Services.Interfaces;
public interface ITrainingService
{
	Task<int> AddTrainingAsync(CreateTrainingDto dto, int userId);
	Task DeleteTrainingAsync(int id, int userId);
	Task<PageResult<TrainingDto>> GetTrainingsPaginatedAsync(TrainingQuery query, int userId);
	Task<FavouriteExercisesDto> GetFavouriteExercisesAsync(int userId);
	Task<IEnumerable<TrainingChartDataDto>> GetTrainingChartDataAsync(TrainingPeriod period, int userId);
	Task<TrainingStatsDto> GetTrainingStatsAsync(int userId);
}
