using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FitnessPortalAPI.Constants;

namespace FitnessPortalAPI.Services
{
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
                        TotalPayload = 0.0f,
                        UserId = userId,
                    };

                    _context.Trainings.Add(training);
                    await _context.SaveChangesAsync();

                    float totalPayload = 0.0f;
                    foreach (var exerciseDto in dto.Exercises)
                    {
                        var exercise = new Exercise()
                        {
                            Name = exerciseDto.Name.ToLower(),
                            NumberOfReps = exerciseDto.NumberOfReps,
                            Payload = exerciseDto.Payload,
                            TrainingId = training.Id
                        };

                        float exercisePayload = dto.NumberOfSeries*exercise.NumberOfReps*exercise.Payload;
                        totalPayload += exercisePayload;

                        _context.Exercises.Add(exercise);

                    }
                    training.TotalPayload = totalPayload;
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return training.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new Exception("An error occurred while adding the training."); 
                }

            }
        }

        public async Task DeleteTraining(int id, int userId)
        {
            var training = await _context.Trainings
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (training == null)
                throw new BadRequestException("Training not found");

            if (training.UserId != userId)
                throw new ForbiddenException("You are not allowed to delete this training");

            _context.Trainings.Remove(training);
            await _context.SaveChangesAsync();
        }

        public async Task<PageResult<TrainingDto>> GetAllTrainingsPaginated(TrainingQuery query, int userId)
        {
            Thread.Sleep(1000);//added to present loading spinner in client app

            var baseQuery = _context.Trainings
                .Include(t => t.Exercises)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.DateOfTraining);

            var trainings = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();

            var totalItemsCount = await baseQuery.CountAsync();

            var trainingsDtos = new List<TrainingDto>();
            for (int i = 0; i < trainings.Count; i++)
            {
                trainingsDtos.Add(new TrainingDto()
                {
                    Id = trainings[i].Id,
                    DateOfTraining = trainings[i].DateOfTraining,
                    NumberOfSeries = trainings[i].NumberOfSeries,
                    TotalPayload = trainings[i].TotalPayload,
                    Exercises = trainings[i].Exercises.Select(exercise => new ExerciseDto()
                    {
                        Name = exercise.Name,
                        NumberOfReps = exercise.NumberOfReps,
                        Payload = exercise.Payload,
                    }).ToList(),
                });
            }

            var result = new PageResult<TrainingDto>(trainingsDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<IEnumerable<TrainingChartDataDto>> GetTrainingChartData(TrainingPeriod period, int userId)
        {
            DateTime startDate;
            DateTime endDate;

            switch (period)
            {
                case TrainingPeriod.Month:
                    startDate = DateTime.Now.AddMonths(-1);
                    endDate = DateTime.Now;
                    break;
                case TrainingPeriod.Quarter:
                    startDate = DateTime.Now.AddMonths(-3);
                    endDate = DateTime.Now;
                    break;
                case TrainingPeriod.HalfYear:
                    startDate = DateTime.Now.AddMonths(-6);
                    endDate = DateTime.Now;
                    break;
                default:
                    throw new BadRequestException("Invalid period value. Supported values are 'month', 'quarter' and 'halfyear'");
            }

            var trainings = await _context.Trainings
                .Where(t => t.UserId == userId && t.DateOfTraining >= startDate && t.DateOfTraining <= endDate)
                .Include(t => t.Exercises)
                .ToListAsync();

            var trainingChartData = trainings
                .OrderBy(training => training.DateOfTraining)
                .Select(training => new TrainingChartDataDto()
            {
                Date = training.DateOfTraining.ToString("yyyy-MM-dd"),
                Payload = training.TotalPayload
            }).ToList();

            return trainingChartData;
        }
        public async Task<TrainingStatsDto> GetTrainingStats(int userId)
        {
            Thread.Sleep(1000); // Added to present loading spinner in the client app
            var user = await _context.Users
                .Include(u => u.Trainings)
                .ThenInclude(t => t.Exercises)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var userTrainingStats = new TrainingStatsDto()
            {
                NumberOfTrainings = user.Trainings.Count,
                BestTraining = user.Trainings
                    .OrderByDescending(t => t.TotalPayload)
                    .Select(t => new TrainingDto()
                    {
                        Id = t.Id,
                        DateOfTraining = t.DateOfTraining,
                        NumberOfSeries = t.NumberOfSeries,
                        TotalPayload = t.TotalPayload,
                        Exercises = t.Exercises.Select(exercise => new ExerciseDto()
                        {
                            Name = exercise.Name,
                            NumberOfReps = exercise.NumberOfReps,
                            Payload = exercise.Payload,
                        }).ToList()
                    })
                    .FirstOrDefault(),
                MostRecentTraining = user.Trainings.
                    OrderByDescending(t => t.DateOfTraining)
                    .Select(t => new TrainingDto()
                    {
                        Id = t.Id,
                        DateOfTraining = t.DateOfTraining,
                        NumberOfSeries = t.NumberOfSeries,
                        TotalPayload = t.TotalPayload,
                        Exercises = t.Exercises.Select(exercise => new ExerciseDto()
                        {
                            Name = exercise.Name,
                            NumberOfReps = exercise.NumberOfReps,
                            Payload = exercise.Payload,
                        }).ToList()
                    })
                    .FirstOrDefault()
            };

            return userTrainingStats;
        }

        public async Task<FavouriteExercisesDto> GetFavouriteExercises(int userId)
        {
            Thread.Sleep(1000); // Added to present loading spinner in the client app

            var userTrainings = await _context.Trainings
                .Where(training => training.UserId == userId)
                .Include(training => training.Exercises)
                .OrderByDescending(training => training.DateOfTraining)
                .Take(3)
                .ToListAsync();

            if (userTrainings.Count < 3)
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

        public async Task<IEnumerable<TrainingDto>> GetFriendTrainings(int userId, int friendId)
        {
            var user = _context.Users
                .Include(f => f.Friends)
                .FirstOrDefault(user => user.Id == userId);

            if (!user!.Friends.Any(f => f.Id == friendId))
                throw new ForbiddenException("You are not allowed to view trainings of this user");

            var friendTrainings = await _context.Trainings
                .Where(t => t.UserId == friendId)
                .Include(t => t.Exercises)
                .ToListAsync();

            var trainingDtos = friendTrainings.Select(training => new TrainingDto()
            {
                Id = training.Id,
                DateOfTraining = training.DateOfTraining,
                NumberOfSeries = training.NumberOfSeries,
                TotalPayload = training.TotalPayload,
                Exercises = training.Exercises.Select(exercise => new ExerciseDto()
                {
                    Name = exercise.Name,
                    NumberOfReps = exercise.NumberOfReps,
                    Payload = exercise.Payload,
                }).ToList()
            }).ToList();

            return trainingDtos;
        }
    }
}
