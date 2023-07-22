using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Training;

namespace FitnessPortalAPI.Services
{
    public interface ITrainingService
    {
        Task<int> AddTraining(CreateTrainingDto dto, int userId);
    }
    public class TrainingService : ITrainingService
    {
        private readonly FitnessPortalDbContext _context;
        public TrainingService(FitnessPortalDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddTraining(CreateTrainingDto dto, int userId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var training = new Training()
                    {
                        DateOfTraining = DateTime.Now,
                        NumberOfSeries = dto.NumberOfSeries,
                        TotalPayload = dto.TotalPayload,
                        UserId = userId,
                    };

                    _context.Trainings.Add(training);
                    await _context.SaveChangesAsync();

                    foreach (var exerciseDto in dto.Exercises)
                    {
                        var exercise = new Exercise()
                        {
                            Name = exerciseDto.Name,
                            NumberOfReps = exerciseDto.NumberOfReps,
                            Payload = exerciseDto.Payload,
                            TrainingId = training.Id
                        };

                        _context.Exercises.Add(exercise);

                    }
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return training.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

            }
            

            throw new NotImplementedException();
        }
    }
}
