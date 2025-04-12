using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Services.Interfaces
{
	public interface ITrainingService
    {
        Task<int> AddTraining(CreateTrainingDto dto, int userId);
        Task DeleteTraining(int id, int userId);
        Task<PageResult<TrainingDto>> GetTrainingsPaginated(TrainingQuery query, int userId);
        Task<FavouriteExercisesDto> GetFavouriteExercises(int userId);
        Task<IEnumerable<TrainingChartDataDto>> GetTrainingChartData(TrainingPeriod period, int userId);
        Task<TrainingStatsDto> GetTrainingStats(int userId);
    }
}
