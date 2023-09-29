using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.DAL.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly FitnessPortalDbContext _dbContext;
        public TrainingRepository(FitnessPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateTrainingAsync(Training training, List<Exercise> exercises)
        {
            using(var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.Trainings.AddAsync(training);
                    await _dbContext.SaveChangesAsync();

                    foreach(var exercise in exercises)
                    {
                        exercise.TrainingId = training.Id;
                        await _dbContext.Exercises.AddAsync(exercise);
                    }

                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();

                    return training.Id;
                } 
                catch(Exception)
                {
                    transaction.Rollback();
                    throw new Exception("An error occured while adding the training.");
                }
            }
        }

        public async Task DeleteTraining(int trainingId)
        {
            var training = await GetTrainingByIdAsync(trainingId);
            if (training != null)
            {
                _dbContext.Trainings.Remove(training);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Training>> GetPaginatedTrainingsForUserAsync(int userId, TrainingQuery query)
        {
            var baseQuery = _dbContext.Trainings
                                .Include(t => t.Exercises)
                                .Where(t => t.UserId != userId)
                                .OrderByDescending(t => t.DateOfTraining);

            var trainings = await baseQuery
                                    .Skip(query.PageSize * (query.PageNumber - 1))
                                    .Take(query.PageSize)
                                    .ToListAsync();

            return trainings;
        }

        public async Task<Training?> GetTrainingByIdAsync(int trainingId)
        {
            return await _dbContext.Trainings
                            .FirstOrDefaultAsync(t => t.Id == trainingId);
        }

        public async Task<IEnumerable<Training>> GetChartDataAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Trainings
                                    .Where(t => t.UserId == userId && t.DateOfTraining >= startDate && t.DateOfTraining <= endDate)
                                    .Include(t => t.Exercises)
                                    .ToListAsync();
        }

        public async Task<int> GetTotalTrainingsCountForUserAsync(int userId)
        {
            return await _dbContext.Trainings
                            .Where(t => t.UserId == userId)
                            .CountAsync();
        }

        public async Task<Training?> GetBestTrainingAsync(int userId)
        {
            return await _dbContext.Trainings
                            .Where(t => t.UserId == userId)
                            .OrderByDescending(t => t.TotalPayload)
                            .FirstOrDefaultAsync();
        }

        public async Task<Training?> GetMostRecentTrainingAsync(int userId)
        {
            return await _dbContext.Trainings
                            .Where(t => t.UserId == userId)
                            .OrderByDescending(t => t.DateOfTraining)
                            .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserWithTrainings(int userId)
        {
            return await _dbContext.Users
                .Include(u => u.Trainings)
                .ThenInclude(t => t.Exercises)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<Training>> GetRecentTrainingsForUserAsync(int userId, int count)
        {
            return await _dbContext.Trainings
                .Where(training => training.UserId == userId)
                .Include(training => training.Exercises)
                .OrderByDescending(training => training.DateOfTraining)
                .Take(count)
                .ToListAsync();
        }
    }
}
