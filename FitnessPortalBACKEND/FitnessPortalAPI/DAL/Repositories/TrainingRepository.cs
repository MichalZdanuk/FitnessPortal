using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.DAL.Repositories;
public class TrainingRepository(FitnessPortalDbContext dbContext)
		: ITrainingRepository
{
	public async Task<int> CreateTrainingAsync(Training training, List<Exercise> exercises)
	{
		using (var transaction = await dbContext.Database.BeginTransactionAsync())
		{
			try
			{
				await dbContext.Trainings.AddAsync(training);
				await dbContext.SaveChangesAsync();

				foreach (var exercise in exercises)
				{
					exercise.TrainingId = training.Id;
					await dbContext.Exercises.AddAsync(exercise);
				}

				await dbContext.SaveChangesAsync();
				transaction.Commit();

				return training.Id;
			}
			catch (Exception)
			{
				transaction.Rollback();
				throw new Exception("An error occured while adding the training.");
			}
		}
	}

	public async Task DeleteTrainingAsync(int trainingId)
	{
		var training = await GetTrainingByIdAsync(trainingId);
		if (training != null)
		{
			dbContext.Trainings.Remove(training);
			await dbContext.SaveChangesAsync();
		}
	}

	public async Task<IEnumerable<Training>> GetPaginatedTrainingsForUserAsync(int userId, TrainingQuery query)
	{
		var baseQuery = dbContext.Trainings
							.Include(t => t.Exercises)
							.Where(t => t.UserId == userId)
							.OrderByDescending(t => t.DateOfTraining);

		var trainings = await baseQuery
								.Skip(query.PageSize * (query.PageNumber - 1))
								.Take(query.PageSize)
								.ToListAsync();

		return trainings;
	}

	public async Task<Training?> GetTrainingByIdAsync(int trainingId)
	{
		return await dbContext.Trainings
						.FirstOrDefaultAsync(t => t.Id == trainingId);
	}

	public async Task<IEnumerable<Training>> GetChartDataAsync(int userId, DateTime startDate, DateTime endDate)
	{
		return await dbContext.Trainings
								.Where(t => t.UserId == userId && t.DateOfTraining >= startDate.AddDays(-1) && t.DateOfTraining <= endDate.AddDays(1))
								.Include(t => t.Exercises)
								.OrderBy(t => t.DateOfTraining)
								.ToListAsync();
	}

	public async Task<int> GetTotalTrainingsCountForUserAsync(int userId)
	{
		return await dbContext.Trainings
						.Where(t => t.UserId == userId)
						.CountAsync();
	}

	public async Task<Training?> GetBestTrainingAsync(int userId)
	{
		return await dbContext.Trainings
						.Where(t => t.UserId == userId)
						.OrderByDescending(t => t.TotalPayload)
						.FirstOrDefaultAsync();
	}

	public async Task<Training?> GetMostRecentTrainingAsync(int userId)
	{
		return await dbContext.Trainings
						.Where(t => t.UserId == userId)
						.OrderByDescending(t => t.DateOfTraining)
						.FirstOrDefaultAsync();
	}

	public async Task<User?> GetUserWithTrainingsAsync(int userId)
	{
		return await dbContext.Users
			.Include(u => u.Trainings)
			.ThenInclude(t => t.Exercises)
			.FirstOrDefaultAsync(u => u.Id == userId);
	}

	public async Task<IEnumerable<Training>> GetRecentTrainingsForUserAsync(int userId, int count)
	{
		return await dbContext.Trainings
			.Where(training => training.UserId == userId)
			.Include(training => training.Exercises)
			.OrderByDescending(training => training.DateOfTraining)
			.Take(count)
			.ToListAsync();
	}
}
