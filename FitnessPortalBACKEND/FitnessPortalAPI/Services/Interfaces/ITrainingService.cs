using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Services.Interfaces;
public interface ITrainingService
{
	Task<int> AddTrainingAsync(CreateTrainingDto dto);
	Task DeleteTrainingAsync(int id);
	Task<PageResult<TrainingDto>> GetTrainingsPaginatedAsync(TrainingQuery query);
	Task<FavouriteExercisesDto> GetFavouriteExercisesAsync();
	Task<IEnumerable<TrainingChartDataDto>> GetTrainingChartDataAsync(TrainingPeriod period);
	Task<TrainingStatsDto> GetTrainingStatsAsync();
}
