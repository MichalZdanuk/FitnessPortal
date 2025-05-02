using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Utils;

namespace FitnessPortalAPI.Services;

public class TrainingService(IAuthenticationContext authenticationContext,
	ITrainingRepository trainingRepository,
	IMapper mapper)
		: ITrainingService
{
	public async Task<int> AddTrainingAsync(CreateTrainingDto dto)
	{
		var training = mapper.Map<Training>(dto);
		training.DateOfTraining = DateTime.Now;
		training.UserId = authenticationContext.UserId;

		var exercises = dto.Exercises.Select(exerciseDto => mapper.Map<Exercise>(exerciseDto)).ToList();

		float totalPayload = 0;

		foreach (var exercise in exercises)
		{
			totalPayload += exercise.NumberOfReps * exercise.Payload;
		}

		training.TotalPayload = totalPayload * dto.NumberOfSeries;

		return await trainingRepository.CreateTrainingAsync(training, exercises);
	}

	public async Task DeleteTrainingAsync(int id)
	{
		var training = await trainingRepository.GetTrainingByIdAsync(id);

		if (training is null)
			throw new BadRequestException("Training not found");

		if (training.UserId != authenticationContext.UserId)
			throw new ForbiddenException("You are not allowed to delete this training");

		await trainingRepository.DeleteTrainingAsync(id);
	}

	public async Task<PageResult<TrainingDto>> GetTrainingsPaginatedAsync(TrainingQuery query)
	{
		var userId = authenticationContext.UserId;

		var trainings = await trainingRepository.GetPaginatedTrainingsForUserAsync(userId, query);
		var totalItemsCount = await trainingRepository.GetTotalTrainingsCountForUserAsync(userId);
		var trainingsDtos = mapper.Map<List<TrainingDto>>(trainings);

		var result = new PageResult<TrainingDto>(trainingsDtos, totalItemsCount, query.PageSize, query.PageNumber);

		return result;
	}

	public async Task<IEnumerable<TrainingChartDataDto>> GetTrainingChartDataAsync(TrainingPeriod period)
	{
		(DateTime startDate, DateTime endDate) = DateRangeHelper.CalculateDateRange(period);

		var trainings = await trainingRepository.GetChartDataAsync(authenticationContext.UserId, startDate, endDate);
		var trainingChartData = mapper.Map<IEnumerable<TrainingChartDataDto>>(trainings);

		return trainingChartData;
	}

	public async Task<TrainingStatsDto> GetTrainingStatsAsync()
	{
		var userId = authenticationContext.UserId;
		var user = await trainingRepository.GetUserWithTrainingsAsync(userId);

		if (user is null)
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

	public async Task<FavouriteExercisesDto> GetFavouriteExercisesAsync()
	{
		var userTrainings = await trainingRepository.GetRecentTrainingsForUserAsync(authenticationContext.UserId, 3);
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
}
