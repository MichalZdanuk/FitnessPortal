using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services.Interfaces;
using FitnessPortalAPI.Constants;
using FitnessPortalAPI.Repositories;
using AutoMapper;

namespace FitnessPortalAPI.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ICalculatorRepository _calculatorRepository;
        private readonly IMapper _mapper;
        private Calculator _calculator = new Calculator();

        public CalculatorService(ICalculatorRepository calculatorRepository, IMapper mapper)
        {
            _calculatorRepository = calculatorRepository;
            _mapper = mapper;
        }

        public async Task<BMIDto> CalculateBMI(CreateBMIQuery dto, int userId)
        {
            var bmiIndex = 0.0f;
            var bmiCategory = BMICategory.Normalweight;
            _calculator.CalculateBMI(dto.Height, dto.Weight, out bmiIndex, out bmiCategory);

            var bmi = new BMI()
            {
                Date = DateTime.Now,
                BMIScore = bmiIndex,
                BMICategory = bmiCategory,
                Height = dto.Height,
                Weight = dto.Weight,
                UserId = userId,
            };

            await _calculatorRepository.AddBmiAsync(bmi);
            var bmiDto = _mapper.Map<BMIDto>(bmi);

            return bmiDto;
        }

        public async Task<BMIDto> CalculateBMIForAnonymous(CreateBMIQuery dto)
        {
            var bmiIndex = 0.0f;
            var bmiCategory = BMICategory.Normalweight;
            _calculator.CalculateBMI(dto.Height, dto.Weight, out bmiIndex, out bmiCategory);
            var bmiDto = new BMIDto()
            {
                Date = DateTime.Now,
                BMIScore = bmiIndex,
                BMICategory = bmiCategory,
            };

            return await Task.FromResult(bmiDto);
        }

        public async Task<PageResult<BMIDto>> GetAllBMIsForUserPaginated(BMIQuery query, int userId)
        {
            Thread.Sleep(1000);//added to present loading spinner in client app

            var bmis = await _calculatorRepository.GetBMIsForUserPaginated(userId, query.PageNumber, query.PageSize);
            var totalItemsCount = await _calculatorRepository.GetTotalBMIsCountForUser(userId);

            var bmiDtos = _mapper.Map<List<BMIDto>>(bmis);

            var result = new PageResult<BMIDto>(bmiDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<BMRDto> CalculateBMRForAnonymous(CreateBMRQuery bmrQuery)
        {
            var bmrResult = _calculator.CalculateBMR(bmrQuery.Height, bmrQuery.Weight, bmrQuery.Age, bmrQuery.Sex);
            var bmrDto = new BMRDto()
            {
                BMRScore = bmrResult,
            };

            return await Task.FromResult(bmrDto);
        }

        public async Task<BodyFatDto> CalculateBodyFatForAnonymous(CreateBodyFatQuery bodyFatQuery)
        {
            var bodyFatResult = _calculator.CalculateBodyFat(bodyFatQuery.Height, bodyFatQuery.Waist, bodyFatQuery.Neck, bodyFatQuery.Hip, bodyFatQuery.Sex);
            var bodyFatDto = new BodyFatDto()
            {
                BodyFatLevel = bodyFatResult,
            };

            return await Task.FromResult(bodyFatDto);
        }
    }
}
