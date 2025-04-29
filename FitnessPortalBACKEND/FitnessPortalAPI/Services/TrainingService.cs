using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Services;

public class TrainingService(ITrainingRepository trainingRepository, IMapper mapper)
		: ITrainingService
{
	public async Task<int> AddTrainingAsync(CreateTrainingDto dto, int userId)
	{
		var training = mapper.Map<Training>(dto);
		training.DateOfTraining = DateTime.Now;
		training.UserId = userId;

		var exercises = dto.Exercises.Select(exerciseDto => mapper.Map<Exercise>(exerciseDto)).ToList();

		float totalPayload = 0;

		foreach (var exercise in exercises)
		{
			totalPayload += exercise.NumberOfReps * exercise.Payload;
		}

		training.TotalPayload = totalPayload * dto.NumberOfSeries;

		return await trainingRepository.CreateTrainingAsync(training, exercises);
	}

	public async Task DeleteTrainingAsync(int id, int userId)
	{
		var training = await trainingRepository.GetTrainingByIdAsync(id);

		if (training == null)
			throw new BadRequestException("Training not found");

		if (training.UserId != userId)
			throw new ForbiddenException("You are not allowed to delete this training");

		await trainingRepository.DeleteTraining(id);
	}

	public async Task<PageResult<TrainingDto>> GetTrainingsPaginatedAsync(TrainingQuery query, int userId)
	{
		var trainings = await trainingRepository.GetPaginatedTrainingsForUserAsync(userId, query);
		var totalItemsCount = await trainingRepository.GetTotalTrainingsCountForUserAsync(userId);
		var trainingsDtos = mapper.Map<List<TrainingDto>>(trainings);
		var result = new PageResult<TrainingDto>(trainingsDtos, totalItemsCount, query.PageSize, query.PageNumber);

		return result;
	}

	public async Task<IEnumerable<TrainingChartDataDto>> GetTrainingChartDataAsync(TrainingPeriod period, int userId)
	{
		(DateTime startDate, DateTime endDate) = CalculateDateRange(period);

		var trainings = await trainingRepository.GetChartDataAsync(userId, startDate, endDate);
		var trainingChartData = mapper.Map<IEnumerable<TrainingChartDataDto>>(trainings);

		return trainingChartData;
	}

	public async Task<TrainingStatsDto> GetTrainingStatsAsync(int userId)
	{
		var user = await trainingRepository.GetUserWithTrainings(userId);

		if (user == null)
		{
			throw new NotFoundException("User not found");
		}

		var bestTraining = await trainingRepository.GetBestTrainingAsync(userId);
		var mostRecentTraining = await trainingRepository.GetMostRecentTrainingAsync(userId);
		var numberOfTrainings = await trainingRepository.GetTotalTrainingsCountForUserAsync(userId);

		var userTrainingStats = new TrainingStatsDto()
		{
			NumberOfTrainings = numberOfTrainings,
			BestTraining = mapper.Map<TrainingDto>(bestTraining),
			MostRecentTraining = mapper.Map<TrainingDto>(mostRecentTraining),
		};

		return userTrainingStats;
	}

	public async Task<FavouriteExercisesDto> GetFavouriteExercisesAsync(int userId)
	{
		var userTrainings = await trainingRepository.GetRecentTrainingsForUserAsync(userId, 3);
		var trainingsList = userTrainings.ToList();

		if (trainingsList.Count < 3)
		{
			return new FavouriteExercisesDto
			{
				Exercises = new List<ExerciseDto>()
			};
		}

		var exerciseTotals = new Dictionary<string, (int NumberOfReps, float Payload)>();
		foreach (var training in userTrainings)
		{
			foreach (var exercise in training.Exercises)
			{
				if (!exerciseTotals.ContainsKey(exercise.Name))
				{
					exerciseTotals[exercise.Name] = (0, 0);
				}
				var currentTuple = exerciseTotals[exercise.Name];
				exerciseTotals[exercise.Name] = (
					currentTuple.NumberOfReps += exercise.NumberOfReps * training.NumberOfSeries,
					currentTuple.Payload += exercise.Payload * exercise.NumberOfReps * training.NumberOfSeries
				);
			}
		}

		var topExercises = exerciseTotals.OrderByDescending(kv => kv.Value.NumberOfReps)
			.Take(3)
			.Select(kv => new ExerciseDto()
			{
				Name = kv.Key,
				NumberOfReps = kv.Value.NumberOfReps,
				Payload = kv.Value.Payload,
			})
			.ToList();

		var favouriteDto = new FavouriteExercisesDto
		{
			Exercises = topExercises,
		};

		return favouriteDto;
	}

	private (DateTime startDate, DateTime endDate) CalculateDateRange(TrainingPeriod period)
	{
		DateTime endDate = DateTime.Now;
		DateTime startDate;

		switch (period)
		{
			case TrainingPeriod.Month:
				startDate = endDate.AddMonths(-1);
				break;
			case TrainingPeriod.Quarter:
				startDate = endDate.AddMonths(-3);
				break;
			case TrainingPeriod.HalfYear:
				startDate = endDate.AddMonths(-6);
				break;
			default:
				throw new BadRequestException("Invalid period value. Supported values are 'month', 'quarter', and 'halfyear'");
		}

		startDate = startDate.Date;
		endDate = endDate.Date;

		return (startDate, endDate);
	}
}
