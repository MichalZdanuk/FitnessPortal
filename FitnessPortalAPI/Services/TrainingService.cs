using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Models.Training;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.Services
{
    public interface ITrainingService
    {
        Task<int> AddTraining(CreateTrainingDto dto, int userId);
        Task DeleteTraining(int id, int userId);
        Task<PageResult<TrainingDto>> GetAllTrainingsPaginated(TrainingQuery query, int userId);
        Task<IEnumerable<TrainingDto>> GetFilteredTrainings(string period, int userId);
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

        public async Task<IEnumerable<TrainingDto>> GetFilteredTrainings(string period, int userId)
        {
            DateTime startDate;
            DateTime endDate;

            switch(period)
            {
                case "week":
                    startDate = DateTime.Now.AddDays(-7);
                    endDate = DateTime.Now;
                    break;
                case "month":
                    startDate = DateTime.Now.AddMonths(-1);
                    endDate = DateTime.Now;
                    break;
                case "quarter":
                    startDate = DateTime.Now.AddMonths(-3);
                    endDate = DateTime.Now;
                    break;
                default:
                    throw new BadRequestException("Invalid period value. Supported values are 'week', 'month' and 'quarter'");
            }

            var trainings = await _context.Trainings
                .Where(t => t.UserId == userId && t.DateOfTraining >= startDate && t.DateOfTraining <= endDate)
                .Include(t => t.Exercises)
                .ToListAsync();

            var trainingDtos = trainings.Select(training => new TrainingDto()
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
