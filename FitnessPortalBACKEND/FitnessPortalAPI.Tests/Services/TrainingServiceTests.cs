using AutoMapper;
using FitnessPortalAPI.Authentication;
using FitnessPortalAPI.Constants;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Mappings;
using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Repositories;
using FitnessPortalAPI.Services;
using NSubstitute;
using Shouldly;

namespace FitnessPortalAPI.Tests.Services
{
    [TestClass]
    public class TrainingServiceTests
    {
        private int userId = 1;
        private IAuthenticationContext _authenticationContext;
        private ITrainingRepository _trainingRepository;
        private IMapper _mapper;
        private TrainingService _trainingService;

        public TrainingServiceTests()
        {
            _authenticationContext = Substitute.For<IAuthenticationContext>();
            _authenticationContext.UserId.Returns(userId);
            _trainingRepository = Substitute.For<ITrainingRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<FitnessPortalMappingProfile>()));
            _trainingService = new TrainingService(_authenticationContext, _trainingRepository, _mapper);
        }

        [TestMethod]
        public async Task AddTraining_ValidInput_ShouldCallRepositoryCreateTrainingAsync()
        {
            // arrange
            var createTrainingDto = new CreateTrainingDto()
            {
                NumberOfSeries = 3,
                Exercises = new List<CreateExerciseDto>()
                {
                    new CreateExerciseDto()
                    {
                        Name = "Exercise1",
                        NumberOfReps = 10,
                        Payload = 50f
                    },
                    new CreateExerciseDto()
                    {
                        Name = "Exercise2",
                        NumberOfReps = 8,
                        Payload = 60f
                    }
                }
            };

            // act
            await _trainingService.AddTrainingAsync(createTrainingDto);

            // assert
            await _trainingRepository.Received(1).CreateTrainingAsync(Arg.Any<Training>(), Arg.Any<List<Exercise>>());
        }

        [TestMethod]
        public async Task DeleteTraining_ExistingTraining_ShouldCallRepositoryDeleteTraining()
        {
            // arrange
            var trainingId = 1;
            var training = new Training()
            {
                Id = trainingId,
                UserId = userId,
            };
            _trainingRepository.GetTrainingByIdAsync(trainingId).Returns(training);

            // act
            await _trainingService.DeleteTrainingAsync(trainingId);

            // assert
            await _trainingRepository.Received(1).DeleteTrainingAsync(trainingId);
        }

        [TestMethod]
        public async Task DeleteTraining_NotExistingTraining_ShouldThrowBadRequestException()
        {
            // arrange
            var invalidTrainingId = 1;
            _trainingRepository.GetTrainingByIdAsync(invalidTrainingId).Returns(Task.FromResult<Training?>(null));

            // act
            async Task Act() => await _trainingService.DeleteTrainingAsync(invalidTrainingId);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Training not found");
        }

        [TestMethod]
        public async Task GetTrainingsPaginated_ValidQuery_ShouldReturnPageResult()
        {
            // arrange
            var query = new TrainingQuery()
            {
                PageNumber = 1,
                PageSize = 10,
            };
            var trainings = Enumerable.Range(1, 10).Select(i => new Training { Id = i }).ToList();
            var totalItemsCount = 10;
            _trainingRepository.GetPaginatedTrainingsForUserAsync(userId, query).Returns(trainings);
            _trainingRepository.GetTotalTrainingsCountForUserAsync(userId).Returns(totalItemsCount);

            // act
            var result = await _trainingService.GetTrainingsPaginatedAsync(query);

            // assert
            result.ShouldNotBeNull();
            result.Items.Count.ShouldBe(trainings.Count);
            result.TotalItemsCount.ShouldBe(totalItemsCount);
        }

        [TestMethod]
        public async Task GetTrainingsPaginated_InvalidQuery_ShouldReturnEmptyPageResult()
        {
            // arrange
            var query = new TrainingQuery()
            {
                PageNumber = 1,
                PageSize = 10,
            };
            var trainings = new List<Training>();
            var totalItemsCount = 0;
            _trainingRepository.GetPaginatedTrainingsForUserAsync(userId, query).Returns(trainings);
            _trainingRepository.GetTotalTrainingsCountForUserAsync(userId).Returns(totalItemsCount);

            // act
            var result = await _trainingService.GetTrainingsPaginatedAsync(query);

            // assert
            result.ShouldNotBeNull();
            result.Items.ShouldBeEmpty();
            result.TotalItemsCount.ShouldBe(totalItemsCount);
        }

        [TestMethod]
        public async Task GetTrainingChartData_ValidData_ShouldReturnTrainingChartData()
        {
            // arrange
            var period = TrainingPeriod.Month;
            var startDate = DateTime.Now.AddMonths(-1).Date;
            var endDate = DateTime.Now.Date;
            var expectedTrainingData = new List<Training>
            {
                new Training { Id = 1, DateOfTraining = startDate.AddDays(-1) },
                new Training { Id = 2, DateOfTraining = startDate.AddDays(-2) },
                new Training { Id = 3, DateOfTraining = startDate.AddDays(-3) }
            };
            _trainingRepository.GetChartDataAsync(userId, startDate, endDate).Returns(expectedTrainingData);

            // act
            var result = await _trainingService.GetTrainingChartDataAsync(period);

            // assert
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count().ShouldBe(expectedTrainingData.Count);
        }

        [TestMethod]
        public async Task GetTrainingChartData_InvalidTrainingPeriod_ShouldThrowBadRequestException()
        {
            // arrange
            var period = (TrainingPeriod)99;

            // act
            async Task Act() => await _trainingService.GetTrainingChartDataAsync(period);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Invalid period value. Supported values are 'month', 'quarter', and 'halfyear'");
        }

        [TestMethod]
        public async Task GetTrainingChartData_NoData_ShouldReturnEmptyList()
        {
            // arrange
            var period = TrainingPeriod.Month;
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;
            var expectedTrainingData = new List<Training>();
            _trainingRepository.GetChartDataAsync(userId, startDate, endDate).Returns(expectedTrainingData);

            // act
            var result = await _trainingService.GetTrainingChartDataAsync(period);

            // assert
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [TestMethod]
        public async Task GetTrainingStats_ValidData_ShouldReturnTrainingStats()
        {
            // arrange
            var user = new User { Id = userId };
            var bestTraining = new Training { Id = 1 };
            var mostRecentTraining = new Training { Id = 2 };
            var totalTrainings = 5;
            _trainingRepository.GetUserWithTrainingsAsync(userId).Returns(user);
            _trainingRepository.GetBestTrainingAsync(userId).Returns(bestTraining);
            _trainingRepository.GetMostRecentTrainingAsync(userId).Returns(mostRecentTraining);
            _trainingRepository.GetTotalTrainingsCountForUserAsync(userId).Returns(totalTrainings);

            // act
            var result = await _trainingService.GetTrainingStatsAsync();

            // assert
            result.ShouldNotBeNull();
            result.NumberOfTrainings.ShouldBe(totalTrainings);
            result.BestTraining.ShouldNotBeNull();
            result.BestTraining.Id.ShouldBe(bestTraining.Id);
            result.MostRecentTraining.ShouldNotBeNull();
            result.MostRecentTraining.Id.ShouldBe(mostRecentTraining.Id);
        }

        [TestMethod]
        public async Task GetTrainingStats_EmptyData_ShouldReturnEmptyTrainingStats()
        {
            // arrange
            _trainingRepository.GetUserWithTrainingsAsync(userId).Returns(new User { Id = userId });

            // act
            var result = await _trainingService.GetTrainingStatsAsync();

            // assert
            result.ShouldNotBeNull();
            result.NumberOfTrainings.ShouldBe(0);
            result.BestTraining.ShouldBeNull();
            result.MostRecentTraining.ShouldBeNull();
        }

        [TestMethod]
        public async Task GetTrainingStats_InvalidUserId_ShouldThrowNotFoundException()
        {
            // arrange
            _trainingRepository.GetUserWithTrainingsAsync(userId).Returns(Task.FromResult<User?>(null));

            // act
            async Task Act() => await _trainingService.GetTrainingStatsAsync();

            // assert
            var exception = await Should.ThrowAsync<NotFoundException>(Act);
            exception.Message.ShouldBe("User not found");
        }

        [TestMethod]
        public async Task GetFavouriteExercises_ValidData_ShouldReturnFavouriteExercises()
        {
            // arrange
            var exercises = new List<Exercise>
            {
                new Exercise { Name = "ExerciseA", NumberOfReps = 10, Payload = 100 },
                new Exercise { Name = "ExerciseB", NumberOfReps = 15, Payload = 150 },
                new Exercise { Name = "ExerciseC", NumberOfReps = 8, Payload = 80 },
                new Exercise { Name = "ExerciseD", NumberOfReps = 12, Payload = 120 },
                new Exercise { Name = "ExerciseE", NumberOfReps = 20, Payload = 200 },
                new Exercise { Name = "ExerciseF", NumberOfReps = 10, Payload = 100 },
            };

            var trainings = new List<Training>
            {
                new Training
                {
                    Id = 1,
                    UserId = userId,
                    NumberOfSeries = 2,
                    Exercises = new List<Exercise> { exercises[0], exercises[1] }
                },
                new Training
                {
                    Id = 2,
                    UserId = userId,
                    NumberOfSeries = 3,
                    Exercises = new List<Exercise> { exercises[2], exercises[3] }
                },
                new Training
                {
                    Id = 3,
                    UserId = userId,
                    NumberOfSeries = 2,
                    Exercises = new List<Exercise> { exercises[4], exercises[5] }
                },
            };

            _trainingRepository.GetRecentTrainingsForUserAsync(userId, 3).Returns(trainings);

            // act
            var result = await _trainingService.GetFavouriteExercisesAsync();

            // assert
            result.ShouldNotBeNull();
            result.Exercises.ShouldNotBeNull();
            result.Exercises.Count.ShouldBe(3);
            result.Exercises[0].Name.ShouldBe("ExerciseE");
            result.Exercises[1].Name.ShouldBe("ExerciseD");
            result.Exercises[2].Name.ShouldBe("ExerciseB");
        }

        [TestMethod]
        public async Task GetFavouriteExercises_NotEnoughData_ShouldReturnEmptyList()
        {
            // arrange
            var training = new Training
            {
                Id = 1,
                UserId = userId,
                NumberOfSeries = 2,
                Exercises = new List<Exercise> { new Exercise { Name = "ExerciseA", NumberOfReps = 10, Payload = 100 } }
            };

            _trainingRepository.GetRecentTrainingsForUserAsync(userId, 3).Returns(new List<Training> { training });

            // act
            var result = await _trainingService.GetFavouriteExercisesAsync();

            // assert
            result.ShouldNotBeNull();
            result.Exercises.ShouldNotBeNull();
            result.Exercises.ShouldBeEmpty();
        }
    }
}
