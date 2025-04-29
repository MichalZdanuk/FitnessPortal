using AutoMapper;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Repositories;
using FitnessPortalAPI.Services;
using FitnessPortalAPI.Services.Interfaces;
using NSubstitute;
using Shouldly;

namespace FitnessPortalAPI.Tests.Services
{
    [TestClass]
    public class CalculatorServiceTests
    {
        private ICalculatorRepository _calculatorRepository;
        private IMapper _mapper;
        private ICalculatorService _calculatorService;
        public CalculatorServiceTests()
        {
            _calculatorRepository = Substitute.For<ICalculatorRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<FitnessPortalMappingProfile>()));
            _calculatorService = new CalculatorService(_calculatorRepository, _mapper);
        }

        [TestMethod]
        public async Task CalculateBMI_ValidInput_ShouldReturnBMIDto()
        {
            // arrange
            var userId = 1;
            var createBMIQuery = new CreateBMIQuery()
            {
                Height = 175f,
                Weight = 70f,
            };

            // act
            await _calculatorService.CalculateBMIAsync(createBMIQuery, userId);

            // assert
            await _calculatorRepository.Received(1).AddBmiAsync(Arg.Is<BMI>(b =>
                b.Height == createBMIQuery.Height &&
                b.Weight == createBMIQuery.Weight &&
                b.UserId == userId
            ));
        }

        [TestMethod]
        public async Task GetAllBMIsForUserPaginated_ValidQuery_ShouldReturnPageResult()
        {
            // arrange
            var userId = 1;
            var bmiQuery = new BMIQuery()
            {
                PageNumber = 1,
                PageSize = 10,
            };

            var bmis = Enumerable.Range(1, 10).Select(i => new BMI { Id = i }).ToList();
            var totalItemsCount = 10;

            _calculatorRepository.GetBMIsForUserPaginated(userId, bmiQuery.PageNumber, bmiQuery.PageSize).Returns(bmis);
            _calculatorRepository.GetTotalBMIsCountForUser(userId).Returns(totalItemsCount);

            // act
            var result = await _calculatorService.GetAllBMIsForUserPaginatedAsync(bmiQuery, userId);

            // assert
            result.ShouldNotBeNull();
            result.Items.Count.ShouldBe(bmis.Count);
            result.TotalItemsCount.ShouldBe(totalItemsCount);
        }
    }
}
