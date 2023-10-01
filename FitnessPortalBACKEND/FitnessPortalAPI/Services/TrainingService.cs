using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services.Interfaces;
using FitnessPortalAPI.Constants;
using FitnessPortalAPI.Repositories;
using AutoMapper;

namespace FitnessPortalAPI.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public TrainingService(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<int> AddTraining(CreateTrainingDto dto, int userId)
        {
            var training = _mapper.Map<Training>(dto);
            training.DateOfTraining = DateTime.Now;
            training.UserId = userId;

            var exercises = dto.Exercises.Select(exerciseDto => _mapper.Map<Exercise>(exerciseDto)).ToList();

            float totalPayload = 0;

            foreach (var exercise in exercises)
            {
                totalPayload += exercise.NumberOfReps * exercise.Payload;
            }

            training.TotalPayload = totalPayload*dto.NumberOfSeries;

            return await _trainingRepository.CreateTrainingAsync(training, exercises);
        }

        public async Task DeleteTraining(int id, int userId)
        {
            var training = await _trainingRepository.GetTrainingByIdAsync(id);

            if (training == null)
                throw new BadRequestException("Training not found");

            if (training.UserId != userId)
                throw new ForbiddenException("You are not allowed to delete this training");

            await _trainingRepository.DeleteTraining(id);
        }

        public async Task<PageResult<TrainingDto>> GetTrainingsPaginated(TrainingQuery query, int userId)
        {
            var trainings = await _trainingRepository.GetPaginatedTrainingsForUserAsync(userId, query);
            var totalItemsCount = await _trainingRepository.GetTotalTrainingsCountForUserAsync(userId);
            var trainingsDtos = _mapper.Map<List<TrainingDto>>(trainings);
            var result = new PageResult<TrainingDto>(trainingsDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<IEnumerable<TrainingChartDataDto>> GetTrainingChartData(TrainingPeriod period, int userId)
        {
            (DateTime startDate, DateTime endDate) = CalculateDateRange(period);

            var trainings = await _trainingRepository.GetChartDataAsync(userId, startDate, endDate);
            var trainingChartData = _mapper.Map<IEnumerable<TrainingChartDataDto>>(trainings);

            return trainingChartData;
        }

        public async Task<TrainingStatsDto> GetTrainingStats(int userId)
        {
            var user = await _trainingRepository.GetUserWithTrainings(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var bestTraining = await _trainingRepository.GetBestTrainingAsync(userId);
            var mostRecentTraining = await _trainingRepository.GetMostRecentTrainingAsync(userId);
            var numberOfTrainings = await _trainingRepository.GetTotalTrainingsCountForUserAsync(userId);

            var userTrainingStats = new TrainingStatsDto()
            {
                NumberOfTrainings = numberOfTrainings,
                BestTraining = _mapper.Map<TrainingDto>(bestTraining),
                MostRecentTraining = _mapper.Map<TrainingDto>(mostRecentTraining),
            };

            return userTrainingStats;
        }

        public async Task<FavouriteExercisesDto> GetFavouriteExercises(int userId)
        {
            var userTrainings = await _trainingRepository.GetRecentTrainingsForUserAsync(userId, 3);
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
                        currentTuple.NumberOfReps += exercise.NumberOfReps*training.NumberOfSeries,
                        currentTuple.Payload += exercise.Payload* exercise.NumberOfReps * training.NumberOfSeries
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
}
