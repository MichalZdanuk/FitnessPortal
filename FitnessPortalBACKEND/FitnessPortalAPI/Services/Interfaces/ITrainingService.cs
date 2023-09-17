using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Models;

namespace FitnessPortalAPI.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<int> AddTraining(CreateTrainingDto dto, int userId);
        Task DeleteTraining(int id, int userId);
        Task<PageResult<TrainingDto>> GetAllTrainingsPaginated(TrainingQuery query, int userId);
        Task<FavouriteExercisesDto> GetFavouriteExercises(int userId);
        Task<IEnumerable<TrainingDto>> GetFilteredTrainings(string period, int userId);
        Task<TrainingStatsDto> GetTrainingStats(int userId);
        Task<IEnumerable<TrainingDto>> GetFriendTrainings(int userId, int friendId);
    }
}
