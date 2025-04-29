using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Services;

public class CalculatorService(ICalculatorRepository calculatorRepository, IMapper mapper)
	: ICalculatorService
{
	public async Task<BMIDto> CalculateBMIAsync(CreateBMIQuery dto, int userId)
	{
		var bmiIndex = 0.0f;
		var bmiCategory = BMICategory.Normalweight;
		Calculator.CalculateBMI(dto.Height, dto.Weight, out bmiIndex, out bmiCategory);

		var bmi = new BMI()
		{
			Date = DateTime.Now,
			BMIScore = bmiIndex,
			BMICategory = bmiCategory,
			Height = dto.Height,
			Weight = dto.Weight,
			UserId = userId,
		};

		await calculatorRepository.AddBmiAsync(bmi);
		var bmiDto = mapper.Map<BMIDto>(bmi);

		return bmiDto;
	}

	public async Task<BMIDto> CalculateBMIForAnonymousAsync(CreateBMIQuery dto)
	{
		var bmiIndex = 0.0f;
		var bmiCategory = BMICategory.Normalweight;
		Calculator.CalculateBMI(dto.Height, dto.Weight, out bmiIndex, out bmiCategory);
		var bmiDto = new BMIDto()
		{
			Date = DateTime.Now,
			BMIScore = bmiIndex,
			BMICategory = bmiCategory,
		};

		return await Task.FromResult(bmiDto);
	}

	public async Task<PageResult<BMIDto>> GetAllBMIsForUserPaginatedAsync(BMIQuery query, int userId)
	{
		var bmis = await calculatorRepository.GetBMIsForUserPaginatedAsync(userId, query.PageNumber, query.PageSize);
		var totalItemsCount = await calculatorRepository.GetTotalBMIsCountForUserAsync(userId);

		var bmiDtos = mapper.Map<List<BMIDto>>(bmis);

		var result = new PageResult<BMIDto>(bmiDtos, totalItemsCount, query.PageSize, query.PageNumber);

		return result;
	}

	public async Task<BMRDto> CalculateBMRForAnonymousAsync(CreateBMRQuery bmrQuery)
	{
		var bmrResult = Calculator.CalculateBMR(bmrQuery.Height, bmrQuery.Weight, bmrQuery.Age, bmrQuery.Sex);
		var bmrDto = new BMRDto()
		{
			BMRScore = bmrResult,
		};

		return await Task.FromResult(bmrDto);
	}

	public async Task<BodyFatDto> CalculateBodyFatForAnonymousAsync(CreateBodyFatQuery bodyFatQuery)
	{
		var bodyFatResult = Calculator.CalculateBodyFat(bodyFatQuery.Height, bodyFatQuery.Waist, bodyFatQuery.Neck, bodyFatQuery.Hip, bodyFatQuery.Sex);
		var bodyFatDto = new BodyFatDto()
		{
			BodyFatLevel = bodyFatResult,
		};

		return await Task.FromResult(bodyFatDto);
	}
}
